using UnityEngine;
using System.Collections;

public abstract class CharacterMovement : MonoBehaviour
{

    [SerializeField]
    protected float moveSpeed = 1.8f;
    
    protected Animator anim;
    
    [SerializeField]
    protected bool controlOverride;

    protected bool _movingNorth;
    protected bool _movingWest;
    protected bool _movingEast;
    protected bool _movingSouth;
    protected bool isMoving;

    protected float dirX;
    protected float dirY;
    

    // determines how much more speed running adds
    [SerializeField]
    protected float runBoost = 2.8f;
    
    // for the turnTo function
    [SerializeField]
    protected float angleLimit = 1f;
    
    // determine if character is running
    private bool isRunning;
    private float runSpeed;
    
    // min and max map bounds. to keep player within the bounds of the map
    private float mapMinX;
    private float mapMinY;
    private float mapMaxX;
    private float mapMaxY;
    
    // sprite renderer component for character. used to dynamically adjust sorting layer
    private SpriteRenderer _spriteRenderer;

    // dynamically adjusts sorting layer to keep character in front of sprites behind him/her
    [SerializeField] private bool smartLayerSort = true;

    // handle control of character
    protected abstract void HandleControlOfCharacter();
    
    protected void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();         
    }

    private void HandleMovingAndNotMovingAnimations()
    {
        // handle animations for when player is moving or not moving
        if (isMoving)
        {
            anim.SetBool("isMoving", true);

            // handle moving north animation
            if (_movingNorth)
            {
                anim.SetFloat("LastMoveY", 1.0f);
                anim.SetFloat("LastMoveX", 0f);
            }
                     
            // handle moving south animation
            if (_movingSouth)
            {
                anim.SetFloat("LastMoveY", -1.0f);
                anim.SetFloat("LastMoveX", 0f);
            }
            
            // handle moving east animation
            if (_movingEast)
            {
                anim.SetFloat("LastMoveX", 1.0f);
                anim.SetFloat("LastMoveY", 0f);
            }
           
            // handle moving west animation
            if (_movingWest)
            {
                anim.SetFloat("LastMoveX", -1.0f);
                anim.SetFloat("LastMoveY", 0f);
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        
        // update animations
        dirX = _movingEast ? 1f : (_movingWest ? -1.0f : 0f);
        dirY = _movingNorth ? 1f : (_movingSouth ? -1.0f : 0f);
        
        anim.SetFloat("MoveX", dirX);
        anim.SetFloat("MoveY", dirY);
    }
    
    
    protected void FixedUpdate()
    {
        // check movement north
        if (_movingNorth)
        {
            MoveNorth();
        }
        
        // check movement south
        if (_movingSouth)
        {
            MoveSouth();
        }
        
        // check movement east
        if (_movingEast)
        {
            MoveEast();
        }
        
        // check movement west
        if (_movingWest)
        {
            MoveWest();
        }
    }
    
    
    private void MoveNorth()
    {
        if (!isRunning)
        {
            transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, runSpeed * Time.deltaTime, 0);
        }
    }

    private void MoveSouth()
    {
        if (!isRunning)
        {
            transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, -runSpeed * Time.deltaTime, 0);
        }
    }

    private void MoveEast()
    {
        if (!isRunning)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(runSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void MoveWest()
    {
        if (!isRunning)
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0); 
        }
        else
        {
            transform.Translate(-runSpeed * Time.deltaTime, 0, 0);
        }
    }

    protected void Update()
    {
        // keep track of if the player is moving or not
        isMoving = (_movingEast || _movingWest || _movingSouth || _movingNorth);
        
        // update sorting layer
        if (smartLayerSort)
        {
            _spriteRenderer.sortingOrder = Mathf.RoundToInt(-(transform.position.y * 10));
        }

        // handle running
        runSpeed = moveSpeed + runBoost;
        
        // handle moving and not moving animations
        HandleMovingAndNotMovingAnimations();
        
        // handle directional movements
        HandleControlOfCharacter();
    }
    
    // Setters and Getters
    
    // clears directional buffer
    public void clearDirectionalBuffer()
    {
        isRunning = false;
        _movingNorth = false;
        _movingWest = false;
        _movingSouth = false;
        _movingEast = false;
    }
    
    // determines if player has control of character
    public void setControlOverride(bool setting)
    {
        clearDirectionalBuffer();
        controlOverride = setting;
    }

    public void setRunning(bool setting)
    {
        isRunning = setting;
    }
    
    // these settings are mainly used for auto movements in event sequences 
    public void setMoveEast(bool setting)
    {
        _movingEast = setting;
    }
    
    public void setMoveNorth(bool setting)
    {
        _movingNorth = setting;
    }
    
    public void setMoveWest(bool setting)
    {
        _movingWest = setting;
    }
    
    public void setMoveSouth(bool setting)
    {
        _movingSouth = setting;
    }

    // gets if player can be controlled
    public bool isUnderPlayerControl()
    {
        return !controlOverride;
    }
    
    // gets angle limit
    public float getAngleLimit()
    {
        return angleLimit;
    }
    
}
