using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager 
{
    private int turncount;
    public event System.Action OnTick;


    public TurnManager() 
    {
    turncount = 1;
    }


    public void Tick()
    {
        turncount += 1;
        OnTick?.Invoke();

    }
}
