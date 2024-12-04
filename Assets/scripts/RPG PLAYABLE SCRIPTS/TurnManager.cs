using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager 
{
    private int turncount;
   

    public TurnManager() 
    {
    turncount = 1;
    }


    public void Nextturn()
    {
        turncount += 1;

    }
}
