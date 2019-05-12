using System;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public CharacterMovement target;
    public float stoppingDistance = .4f;
    public CharacterMovement cm;
    private double angle;
    private Vector2 pos;
    private Vector2 targetPos;
    
    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        cm.setIsFollowing(true);
        target = target == null ? FindObjectOfType<PlayerMovement>() : target;
        cm.setMoveSpeed(target.getMoveSpeed()); 
    }
    
    void Update()
    {
        targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
        pos = new Vector2(transform.position.x, transform.position.y);        
        angle = Math.Abs(Math.Atan2(pos.y - targetPos.y, pos.x - targetPos.x) * 180f / Math.PI); 
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > stoppingDistance)
        {
            cm.setRunning(target.getIsRunning());
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, target.getMoveSpeed() * Time.deltaTime);
            handleAnimation();
        }
        else
        {
            cm.clearDirectionalBuffer(10);
        }
    }

    void handleAnimation()
    {
        if (angle <= 45f)
        {
            cm.setMoveEast(false);
            cm.setMoveWest(true);
            cm.setMoveNorth(false);
            cm.setMoveSouth(false);
        } else if (angle >= 120f)
        {
            cm.setMoveEast(true);
            cm.setMoveWest(false);
            cm.setMoveNorth(false);
            cm.setMoveSouth(false);
        }
        else
        {
            if (targetPos.y >= pos.y)
            {
                cm.setMoveEast(false);
                cm.setMoveWest(false);
                cm.setMoveNorth(true);
                cm.setMoveSouth(false);
            }
            else
            {
                cm.setMoveEast(false);
                cm.setMoveWest(false);
                cm.setMoveNorth(false);
                cm.setMoveSouth(true);
            }
        }
    }
}
