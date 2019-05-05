using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkFromDrake : EventSequence
{

    public override void run()
    {
        walkEast(player, 2f);
        walkNorth(player, 2f);
    }
}
