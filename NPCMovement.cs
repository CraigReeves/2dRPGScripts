using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : CharacterMovement
{    
    protected override void HandleControlOfCharacter()
    {
    }

    void Start()
    {
        base.Start();
        controlOverride = true;        
    }
}
