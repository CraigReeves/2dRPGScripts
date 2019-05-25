using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{

    private bool playerExists;
    
    void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);    
    }
    
    private void Update()
    {
        base.Update();
        
        // for now, allow player to quit on escape
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        // handle running
        if (Input.GetKeyDown(KeyCode.Space))
        {
            setRunning(true);
        } 
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            setRunning(false);
        }
    }
        
    protected override void HandleControlOfCharacter()
    {
        // handle player movement 
        if (!controlOverride)
        {
            _movingSouth = Input.GetKey(KeyCode.DownArrow);
            _movingNorth = Input.GetKey(KeyCode.UpArrow);
            _movingEast = Input.GetKey(KeyCode.RightArrow);
            _movingWest = Input.GetKey(KeyCode.LeftArrow);  
        }  
    }    
}
