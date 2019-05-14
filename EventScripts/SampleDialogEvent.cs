﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleDialogEvent : EventSequence
{
    private GameObject npc;
    private Wander exampleEvent;

    public void Start()
    {
        base.Start();
        npc = npcs[0];
        exampleEvent = GetComponent<Wander>();
    }
    
    public override void run()
    {
        switch ((string) gameWorld.gameState["initState"])
        {
            case "DrakeArrived":
                remotePause(exampleEvent);
                stealControl(player);
                msg("Woman", "Please don't come back in here until you settle \nwhatever beef you have with the dude that just \nbarged in.");
                wait();
                msgClose();
                returnControl(player);
                break;
            case "WaitingOnDrake":
                remotePause(exampleEvent);
                stealControl(player);
                turnToFace(npc, player);
                msg("Woman", "You better go outside right now and stay out of my house.");
                wait();
                msgClose();
                remoteResumeSeq(exampleEvent);
                returnControl(player);
                break;
            default:
                remotePause(exampleEvent);
                stealControl(player);
                turnToFace(npc, player);
                msg("Woman", "Hi there!");
                msgNext();
                promptWin("What do you want to do?", "Go Out", "Stay Home", "Write Music");
                waitForPrompt();
                msgClose();
                returnControl(player);
                break;
        }
    }
}
