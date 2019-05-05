using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Wander : EventSequence
{

    private GameObject npc;
    private Random r;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        npc = npcs[0];
        r = new Random();
    }


    private float randomFloat(int low, int high)
    {
        return r.Next(low, high);
    }

    public override void run()
    {
        walkNorth(npc, 1);
        delay(2);
        walkWest(npc, randomFloat(1, 1));
        delay(randomFloat(1, 4));
        walkEast(npc, randomFloat(1, 1));
        delay(randomFloat(1, 4));
        walkSouth(npc, 1);
        delay(2);
    }
}
