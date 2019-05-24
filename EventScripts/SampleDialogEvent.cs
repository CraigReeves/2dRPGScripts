using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleDialogEvent : EventSequence
{
    private CharacterMovement npc;
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
                stealControl(player());
                msg("Woman", "Please don't come back in here until you settle \nwhatever beef you have with the dude that just \nbarged in.");
                wait();
                msgClose();
                returnControl(player());
                break;
            case "WaitingOnDrake":
                remotePause(exampleEvent);
                stealControl(player());
                turnToFace(npc, player());
                msg("Woman", "You better go outside right now and stay out of my house.");
                wait();
                msgClose();
                remoteResumeSeq(exampleEvent);
                returnControl(player());
                break;
            default:
                remotePause(exampleEvent);
                stealControl(player());
                turnToFace(npc, player());
                msg("Woman", "Hi there!");
                wait();
                msg("Woman", "Unity is cool, isn't it?");
                msgNext();
                picMsg("Slyvian", "Definitely! It's great!", player(), 0);
                msgNext();
                msg("Woman", "Do you want to dance?");
                msgNext();
                wannaDance(); // give player choice to dance
                break;
        }

        void wannaDance()
        {
            promptWin("Dance with Martha?", "Sure", "Nope", "Make it Snow!", "I don't even know you!");
            waitForPrompt((result) =>
            {
                switch (result)
                {
                    default:
                        msg("Woman", "Oh cool! I knew you thought I was too cute \nto pass up!");
                        msgNext();
                        returnControl(player());
                        remoteResumeSeq(exampleEvent);
                        break;
                    case 2:
                        msg("Woman", "That's fine. Whatever.");
                        msgNext();
                        returnControl(player());
                        remoteResumeSeq(exampleEvent);
                        break;
                    case 3:
                        msg("Woman", "No problem!");
                        msgNext();
                        setSnow(true);
                        setFog(true);
                        returnControl(player());
                        remoteResumeSeq(exampleEvent);
                        break;
                    case 4:
                        msg("Woman", "Uh, dude...");
                        wait();
                        msg("Woman", "I don't know if it's dawned on you, but \nI don't know you either.");
                        wait();
                        msg("Woman", "The point of a singles situation for people who don't know each other to do so.");
                        msgNext();
                        wannaDance();
                        break;
                }
            });
        }
    }
}
