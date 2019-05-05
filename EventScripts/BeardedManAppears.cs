using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardedManAppears : EventSequence
{
    private GameObject beardedMan;
    private GameObject woman;
    private Wander wanderEvent;
    
    public void Start()
    {
        base.Start();
        beardedMan = npcs[1];
        woman = npcs[0];
        wanderEvent = FindObjectOfType<Wander>();
    }
    
    public override void run()
    {
        stealControl(player);
        remotePause(wanderEvent);
        faceSouth(woman);
        faceSouth(player);
        delay(1);
        picMsg("Slyvian", "Who is that?", player, 0);
        wait();
        msgClose();
        msg("Woman", "No clue.");
        wait();
        msg("Woman", "Come in!");
        wait();
        msgClose();
        delay(1);
        positionCharacter(beardedMan, 265.29f, 253.05f);
        faceNorth(beardedMan);
        showCharacter(beardedMan);
        positionCharacter(beardedMan, 265.29f, 253.05f);
        walkNorth(beardedMan, 1f);
        delay(2);
        msg("???", "Hello, you two.", 126);
        wait();
        msgClose();
        delay(1);
        faceNorth(player);
        picMsg("Slyvian", "Martha, who is this?", 126, player, 0);
        wait();
        picMsg("Slyvian", "Surely, you wouldn't let just \nany-old-body up in your house!", 126, player, 0);
        wait();
        msgClose();
        msg("Woman", "Hey man, don't look at me. I have no clue who he is.", -137);
        wait();
        msgClose();
        faceSouth(player);
        msg("Man", "If you must know, my name is Drake.", 126);
        wait();
        msg("Drake", "You happen to know of my daughter...", 126);
        wait();
        msgClose();
        delay(1);
        faceNorth(player);
        delay(1);
        faceSouth(player);
        delay(1);
        picMsg("Slyvian", "Daughter?", 126, player, 0);
        wait();
        msgClose();
        msg("Drake", "Lory. Lory Lyon.", 126);
        wait();
        msgClose();
        delay(1);
        faceNorth(player);
        delay(1);
        faceSouth(player);
        picMsg("Slyvian", "Sir, I...", 126, player, 0);
        wait();
        msgClose();
        msg("Drake", "Save it. She told me everything.", 126);
        wait();
        msgClose();
        picMsg("Slyvian", "This isn't what you think!", 126, player, 0);
        wait();
        picMsg("Slyvian", "Whether you want to hear this or not, \nyour daughter is lying to you!", 126, player, 0);
        wait();
        msgClose();
        msg("Drake", "I can assure you that calling my one and only \ndaughter a liar is absolutely NOT going to help \nyou right now.", 126);
        wait();
        msgClose();
        runSouth(woman, 1f);
        msg("Woman", "Hey!", -137);
        wait();
        msg("Woman", "I don't know what the fuck is going on, \nbut you're not about to threaten people in my \nhouse!", -137);
        wait();
        msgClose();      
        msg("Drake", "This isn't a threat. This is a promise.", 126);
        wait();
        msgClose();
        delay(1.5f);
        msg("Drake", "If I hear one more word about you from my \ndaughter, I will fucking kill you.", 126);
        wait();
        msg("Drake", "But have it your way. You wanna take this \noutside then, Slyvian?", 126);
        wait();
        msgClose();
        picMsg("Slyvian", "I have no problem with that!", 126, player, 0);
        wait();
        msgClose();
        msg("Drake", "Good. I'll be waiting.", 126);
        wait();
        msgClose();
        walkSouth(beardedMan, 1f);
        hideCharacter(beardedMan);
        positionCharacter(beardedMan, 0, 0);
        delay(1.5f);
        returnControl(player);
        gameWorld.initState = "DrakeArrived";
    }
}
