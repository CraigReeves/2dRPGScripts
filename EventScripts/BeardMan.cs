using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardMan : EventSequence
{

    private GameObject npc;
    private Wander wanderScript;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        npc = npcs[0];
        wanderScript = GetComponent<Wander>();
    }

    public override void run()
    {
        stealControl(player);
        remotePause(wanderScript);
        turnToFace(npc, player);
        msg("Man", "Your engine is coming along, you know.");
        wait();
        msg("Craig","Thanks!");
        wait();
        msgClose();
        remoteResumeSeq(wanderScript);
        returnControl(player);
    }
}
