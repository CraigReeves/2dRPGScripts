using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherEvent : EventSequence
{
    private ExampleEvent exampleEvent;
    
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        exampleEvent = GetComponent<ExampleEvent>();
    }

    public override void run()
    {
        remotePause(exampleEvent);
        walkEast(player, 2f);
        walkNorth(player, 2f);
        returnControl(player);
        remoteResumeSeq(exampleEvent);
    }
}
