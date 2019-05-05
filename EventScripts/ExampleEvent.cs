using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEvent : EventSequence
{
    private GameObject npc;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        npc = npcs[0];
    }

    public override void run()
    {
        walkSouth(npc, 1f);
        delay(1f);
        faceEast(npc);
        delay(1);
        faceWest(npc);
        delay(1);
        walkNorth(npc, 1f);
        delay(3f);
        
    }    
}
