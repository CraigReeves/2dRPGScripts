﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toOutsideFromInit : EventSequence
{
    
    public override void run()
    {        
        switch ((string) gameWorld.gameState["initState"])
        {
            case "intro":
                stealControl(player());
                picMsg("Slyvian", "Maybe I should at least speak to the lady.", 126, player(), 0);
                wait();
                msgClose();
                returnControl(player());
                break;
            case "DrakeArrived":
                goToScene(262.58f, 257.4f, player(), true);
                break;
            default:
                setNextWeather(true, false, true, true);
                goToScene(262.58f, 257.4f, player(), false);
                break;
        }
    }
}
