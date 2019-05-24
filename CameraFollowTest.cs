using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTest : EventSequence
{
    public override void run()
    {
        switch ((string) gameWorld.gameState["initState"])
        {
            case "DrakeArrived":
                positionCharacter(npcs[0], 269.88f,253.25f);
                faceNorth(npcs[0]);
                showCharacter(npcs[0]);
                faceSouth(player());
                delay(1.5f);
                msg("Drake", "Hey!");
                wait();
                msgClose();
                changeCameraSpeed(1f);
                changeCamFollowTarget(npcs[0].gameObject);
                delay(1.5f);
                walkNorth(npcs[0], 2f);
                msg("Drake", "I'm over here, Slyvian.", 126);
                wait();
                msg("Drake", "Bring your ass down here.", 126);
                wait();
                msgClose();
                changeCamFollowTarget((player().gameObject));
                picMsg("Slyvian", "I don't know what you're doing all the way down there.", player(), 0);
                wait();
                picMsg("Slyvian", "But you were engaging in all that tough talk \nbefore and now you're making me walk.", player(), 0);
                wait();
                msgClose();
                changeCameraSpeed(16f);
                gameWorld.gameState["initState"] = "WaitingOnDrake";
                returnControl(player());
                break;
            case "WaitingOnDrake":
                positionCharacter(npcs[0], 263.67f,257.07f);
                faceWest(npcs[0]);
                showCharacter(npcs[0]);
                break;
        }
        
    }
}
