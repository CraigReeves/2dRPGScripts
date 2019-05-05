﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class EventEngine : MonoBehaviour
{
    private GameObject gameObjectParam;
    private float[] floatParams;
    private string[] stringParams;
    private string name;
    private bool isDone;
    private Vector3 characterDestination;
    private bool autoMoving;
    private float endTime;
    private GoToScene gts;
    private CameraController camera;
    private GameWorld gameWorld;

    public bool start(Command command)
    {        
        var commandName = command.getName();
        gts = FindObjectOfType<GoToScene>();
        gameWorld = FindObjectOfType<GameWorld>();

        camera = FindObjectOfType<CameraController>();

        if (commandName == "walkEast")
        {
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            var dist = command.getFloatParameters()[0];
            return autoMove(cm, "east", dist, false);
        }
        
        if (commandName == "walkWest")
        {
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            var dist = command.getFloatParameters()[0];
            return autoMove(cm, "west", dist, false);
        }

        if (commandName == "walkSouth")
        {
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            var dist = command.getFloatParameters()[0];
            return autoMove(cm, "south", dist, false);
        }
        
        if (commandName == "walkNorth")
        {
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            var dist = command.getFloatParameters()[0];
            return autoMove(cm, "north", dist, false);
        }

        if (commandName == "returnControl")
        {
            var es = command.getEventSequenceParam();
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            return returnControl(cm, es);
        }

        if (commandName == "stealControl")
        {
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            return stealControl(cm);
        }

        if (commandName == "faceNorth")
        {
            var anim = command.getGameObject().GetComponent<Animator>();
            return faceNorth(anim);
        }
        
        if (commandName == "faceWest")
        {
            var anim = command.getGameObject().GetComponent<Animator>();
            return faceWest(anim);
        }
        
        if (commandName == "faceSouth")
        {
            var anim = command.getGameObject().GetComponent<Animator>();
            return faceSouth(anim);
        }
        
        if (commandName == "faceEast")
        {
            var anim = command.getGameObject().GetComponent<Animator>();
            return faceEast(anim);
        }

        if (commandName == "delay")
        {
            var time = command.getFloatParameters()[0];
            return delay(time);
        }

        if (commandName == "pause")
        {
            var eventWorker = command.getEventWorkerParameter();
            return pause(eventWorker);
        }

        if (commandName == "remoteResumeSeq")
        {
            var eventSequence = command.getEventSequenceParam();
            return remoteResumeSeq(eventSequence);
        }

        if (commandName == "remotePause")
        {
            var eventSequence = command.getEventSequenceParam();
            return remotePause(eventSequence);
        }
        
        if (commandName == "remoteCancelSeq")
        {
            var eventSequence = command.getEventSequenceParam();
            return remoteCancelSeq(eventSequence);
        }
        
        if (commandName == "remoteRunSeq")
        {
            var eventSequence = command.getEventSequenceParam();
            return remoteRunSeq(eventSequence);
        }

        if (commandName == "msg")
        {
            var name = command.getStringParameters()[0];
            var message = command.getStringParameters()[1];
            var dialogManager = command.getDialogManagerParam();
            return msg(dialogManager, name, message);
        }
        
        if (commandName == "msgWithHeight")
        {
            var name = command.getStringParameters()[0];
            var message = command.getStringParameters()[1];
            var height = command.getFloatParameters()[0];
            var dialogManager = command.getDialogManagerParam();
            return msg(dialogManager, name, message, height);
        }

        if (commandName == "picMsg")
        {
            var name = command.getStringParameters()[0];
            var message = command.getStringParameters()[1];
            var dialogManager = command.getDialogManagerParam();
            var character = command.getGameObject();
            var avatarIndex = command.getIntParameters()[0];
            return picMsg(dialogManager, name, message, character, avatarIndex);
        }
        
        if (commandName == "picMsgWithHeight")
        {
            var name = command.getStringParameters()[0];
            var message = command.getStringParameters()[1];
            var dialogManager = command.getDialogManagerParam();
            var character = command.getGameObject();
            var avatarIndex = command.getIntParameters()[0];
            var height = command.getFloatParameters()[0];
            return picMsg(dialogManager, name, message, height, character, avatarIndex);
        }

        if (commandName == "msgClose")
        {
            var dm = command.getDialogManagerParam();
            return msgClose(dm);
        }

        if (commandName == "wait")
        {
            var eventWorker = command.getEventWorkerParameter();
            return wait(eventWorker);
        }

        if (commandName == "goToScene")
        {
            var scene = command.getSceneParam();
            var x = command.getFloatParameters()[0];
            var y = command.getFloatParameters()[1];
            var partOfSequence = command.getBoolParameters()[0];
            var player = command.getGameObject();
            return goToScene(scene, x, y, player, partOfSequence);
        }

        if (commandName == "runEast")
        {
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            var dist = command.getFloatParameters()[0];
            return autoMove(cm, "east", dist, true);
        }
        
        if (commandName == "runSouth")
        {
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            var dist = command.getFloatParameters()[0];
            return autoMove(cm, "south", dist, true);
        }
        
        if (commandName == "runWest")
        {
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            var dist = command.getFloatParameters()[0];
            return autoMove(cm, "west", dist, true);
        }
        
        if (commandName == "runNorth")
        {
            var cm = command.getGameObject().GetComponent<CharacterMovement>();
            var dist = command.getFloatParameters()[0];
            return autoMove(cm, "north", dist, true);
        }

        if (commandName == "showCharacter")
        {
            var sprite = command.getGameObject().GetComponent<SpriteRenderer>();
            return showOrHideCharacter(sprite, true);
        }
        
        if (commandName == "hideCharacter")
        {
            var sprite = command.getGameObject().GetComponent<SpriteRenderer>();
            return showOrHideCharacter(sprite, false);
        }

        if (commandName == "positionCharacter")
        {
            var x = command.getFloatParameters()[0];
            var y = command.getFloatParameters()[1];
            var character = command.getGameObject();

            return positionCharacter(character, x, y);
        }

        if (commandName == "triggerAnimation")
        {
            var trigger = command.getStringParameters()[0];
            var anim = command.getGameObject().GetComponent<Animator>();
            return triggerAnimation(anim, trigger);
        }

        if (commandName == "changeCameraFollowTarget")
        {
            var newTarget = command.getGameObject();
            return changeCameraFollowTarget(newTarget);
        }
        
        if (commandName == "changeCameraSpeed")
        {
            var newSpeed = command.getFloatParameters()[0];
            return changeCameraSpeed(newSpeed);
        }

        return true;
    }
    
    private bool stealControl(CharacterMovement cm)
    {
        cm.setControlOverride(true);
        return !cm.isUnderPlayerControl();
    }

    private bool returnControl(CharacterMovement cm, EventSequence es)
    {
        cm.setControlOverride(false);
        es.updateWithinZone(cm.gameObject);
        return cm.isUnderPlayerControl();
    }
    
    private bool autoMove(CharacterMovement cm, string direction, float distance, bool running)
    {
        var destinationReached = false;
        
        // get player position
        var pos = cm.transform.position;
        
        if (direction == "east")
        {
            if (!autoMoving)
            {
                characterDestination = new Vector3(pos.x + distance, pos.y, pos.z);
                autoMoving = true;
            }
            
            // determine if destination has been reached
            destinationReached = pos.x >= characterDestination.x;

            if (!destinationReached)
            {
                cm.setControlOverride(true);
                
                // determine if running
                if (running)
                {
                    cm.setRunning(true);
                }
                
                cm.setMoveEast(true);
            }
            else
            {
                cm.setMoveEast(false);
                cm.setRunning(false);
                autoMoving = false;
            }
        }
        
        if (direction == "north")
        {
            if (!autoMoving)
            {
                characterDestination = new Vector3(pos.x, pos.y + distance, pos.z);
                autoMoving = true;
            }
            
            // determine if destination has been reached
            destinationReached = pos.y >= characterDestination.y;

            if (!destinationReached)
            {
                cm.setControlOverride(true);
                
                // determine if running
                if (running)
                {
                    cm.setRunning(true);
                }
                
                cm.setMoveNorth(true);
            }
            else
            {
                cm.setMoveNorth(false);
                cm.setRunning(false);
                autoMoving = false;
            }
        }
        
        if (direction == "south")
        {
            if (!autoMoving)
            {
                characterDestination = new Vector3(pos.x, pos.y - distance, pos.z);
                autoMoving = true;
            }
            
            // determine if destination has been reached
            destinationReached = pos.y <= characterDestination.y;

            if (!destinationReached)
            {
                cm.setControlOverride(true);
                
                // determine if running
                if (running)
                {
                    cm.setRunning(true);
                }
                
                cm.setMoveSouth(true);
            }
            else
            {
                cm.setMoveSouth(false);
                cm.setRunning(false);
                autoMoving = false;
            }
        }
        
        if (direction == "west")
        {
            if (!autoMoving)
            {
                characterDestination = new Vector3(pos.x - distance, pos.y, pos.z);
                autoMoving = true;
            }
            
            // determine if destination has been reached
            destinationReached = pos.x <= characterDestination.x;

            if (!destinationReached)
            {
                cm.setControlOverride(true);
                
                // determine if running
                if (running)
                {
                    cm.setRunning(true);
                }
                
                cm.setMoveWest(true);
            }
            else
            {
                cm.setMoveWest(false);
                cm.setRunning(false);
                autoMoving = false;
            }
        }

        return destinationReached;
    }

    private bool faceNorth(Animator anim)
    {
        anim.SetFloat("LastMoveY", 1.0f);
        anim.SetFloat("LastMoveX", 0f);
        return anim.GetFloat("LastMoveY") >= 1.0f;
    }
    
    private bool faceSouth(Animator anim)
    {
        anim.SetFloat("LastMoveY", -1.0f);
        anim.SetFloat("LastMoveX", 0f);
        return anim.GetFloat("LastMoveY") <= 1.0f;
    }
    
    private bool faceEast(Animator anim)
    {
        anim.SetFloat("LastMoveX", 1.0f);
        anim.SetFloat("LastMoveY", 0f);
        return anim.GetFloat("LastMoveX") >= 1.0f;
    }
    
    private bool faceWest(Animator anim)
    {
        anim.SetFloat("LastMoveX", -1.0f);
        anim.SetFloat("LastMoveY", 0f);
        return anim.GetFloat("LastMoveX") <= 1.0f;
    }

    private bool delay(float time)
    {
        
        if (endTime <= 0)
        {
            endTime = Time.fixedTime + time;
        }

        var currentTime = Time.fixedTime;

        if (currentTime >= endTime)
        {
            endTime = 0;
            return true;
        }
        
        return false;
    }

    private bool pause(EventWorker worker)
    {
        worker.pauseNow();
        return true;
    }

    private bool remoteResumeSeq(EventSequence es)
    {
        es.resumeSeq();
        return true;
    }

    private bool remotePause(EventSequence es)
    {
        es.pauseNow();
        return true;
    }

    private bool msg(DialogManager dm, string name, string message)
    {
        dm.dialog(name, message);
        return dm.getIsRunning();
    }

    private bool msg(DialogManager dm, string name, string message, float height)
    {
        dm.dialog(name, message, height);
        return dm.getIsRunning();
    }

    private bool picMsg(DialogManager dm, string name, string message, GameObject character, int avatarIndex)
    {
        // get sprite of avatar
        var avatar = character.GetComponent<Player>().getPlayerData().getAvatars()[avatarIndex];
        
        dm.picDialog(name, message, avatar);
        return dm.getIsRunning();
    }
    
    private bool picMsg(DialogManager dm, string name, string message, float height, GameObject character, int avatarIndex)
    {
        // get sprite of avatar
        var avatar = character.GetComponent<Player>().getPlayerData().getAvatars()[avatarIndex];
        
        dm.picDialog(name, message, avatar, height);
        return dm.getIsRunning();
    }

    private bool msgClose(DialogManager dm)
    {
        dm.endDialog();
        dm.endPicDialog();
        return !dm.getIsRunning();
    }
    
    private bool wait(EventWorker ew)
    {
        ew.setWaitForKey(true);
        return true;
    }

    private bool goToScene(Scene scene, float x, float y, GameObject player, bool partOfSequence)
    {
        gts.setLevelToLoad(scene);
        gts.setAsEvent(partOfSequence);
        gameWorld.setAutoReturnControl(!partOfSequence);
        gts.setPosition(x, y);
        gts.setPlayer(player.GetComponent<CharacterMovement>());
        gts.go();

        return true;
    }

    private bool showOrHideCharacter(SpriteRenderer sprite, bool show)
    {
        if (show)
        {
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }

        return true;
    }

    private bool positionCharacter(GameObject character, float x, float y)
    {
        character.transform.position = new Vector3(x, y, character.transform.position.z);
        return true;
    }

    private bool remoteCancelSeq(EventSequence eventSequence)
    {
        eventSequence.cancelSequence();
        return true;
    }

    private bool remoteRunSeq(EventSequence eventSequence)
    {
        eventSequence.run();
        return true;
    }

    private bool triggerAnimation(Animator anim, string trigger)
    {
        anim.SetTrigger(trigger);
        return true;
    }

    private bool changeCameraFollowTarget(GameObject newTarget)
    {
        camera.setFollowTarget(newTarget);
        return true;
    }

    private bool changeCameraSpeed(float newSpeed)
    {
        camera.setCamSpeed(newSpeed);
        return true;
    }
}