using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TreasureChest : EventSequence
{
    private bool withinZone;
    private Animator anim;
    public bool locked;
    public Treasure treasure;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }
    
    // determine if treasure has been obtained
    public bool treasureObtained()
    {
        return gameWorld.getObtainedTreasures().Contains(treasure);
    }

    private void Update()
    {
        base.Update();
        
        if (treasureObtained())
        {
            anim.SetTrigger("open");
        }
    }

    public override void run()
    {        
        if (!locked)
        {
            if (!treasureObtained())
            {
                stealControl(player);
                gameWorld.addTreasure(treasure);
                delay(0.5f);
                msg("", "Got " + treasure.name, -137f);
                msgNext();
                returnControl(player);
            }
        }
        else
        {
            stealControl(player);
            msg("", "Locked.");
            msgNext();
            returnControl(player);
        }
    }
    
}
