using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private GameObject followTarget;

    // position of followTarget
    private Vector3 targetPosition;

    [SerializeField]
    private float moveSpeed = 170;

    //[SerializeField]
    private BoxCollider2D boundBox;

    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Vector3 initialCamCoords;
    
    // if map is narrower than camera, camera does not move horizontally but is centered with map
    private bool restrictX;
    
    // if map is shorter than camera, camera does not move vertically but is centered with map
    private bool restrictY;

    private Camera camera;
    private float halfHeight;
    private float halfWidth;

    private float fullHeight;
    private float fullWidth;

    private float boundWidth;
    private float boundHeight;

    // Start is called before the first frame update
    void Start()
    {
        // get bound box
        boundBox = GameObject.Find("CameraBounds").GetComponent<BoxCollider2D>();
        
        var bounds = boundBox.bounds;
        minBounds = bounds.min;
        maxBounds = bounds.max;

        camera = GetComponent<Camera>();
        halfHeight = camera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
        fullHeight = halfHeight * 2;
        fullWidth = halfWidth * 2;

        boundWidth = maxBounds.x - minBounds.x;
        boundHeight = maxBounds.y - minBounds.y;
        
        // determine whether to restrict X or restrict Y (or both)
        restrictX = fullWidth > boundWidth;
        restrictY = fullHeight > boundHeight;
        
        // if there are restrictions, center map onto screen
        var initX = restrictX ? (minBounds.x + halfWidth) - ((fullWidth - boundWidth) / 2) : 0f;
        var initY = restrictY ? (minBounds.y + halfHeight) - ((fullHeight - boundHeight) / 2) : 0f;
        
        initialCamCoords = new Vector3(initX, initY, transform.position.z); 
    }

    public void setFollowTarget(GameObject gameObject)
    {
        followTarget = gameObject;
    }

    public void setCamSpeed(float speed)
    {
        moveSpeed = speed;
    }
    
    public float getCamSpeed()
    {
        return moveSpeed;
    }

    void Update()
    {        
        // update follow target position
        targetPosition = new Vector3(
            (!restrictX ? followTarget.transform.position.x : initialCamCoords.x), 
            (!restrictY ? followTarget.transform.position.y : initialCamCoords.y), 
            initialCamCoords.z);
    }

    public void updateCamSpeed(float speed)
    {
        moveSpeed = speed;
    }
    

    private void FixedUpdate()
    {
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        var clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        var clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        
        transform.position = new Vector3((!restrictX ? clampedX : transform.position.x), (!restrictY ? clampedY : transform.position.y), transform.position.z);
    }
}