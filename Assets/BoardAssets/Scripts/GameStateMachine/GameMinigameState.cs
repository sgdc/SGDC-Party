using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMinigameState : GameBaseState
{
    
    public override void EnterState(GameStateManager game)
    {
        stateName = "GameMinigameState";
        Debug.Log("Entering State: " + stateName);
    }

    public override void UpdateState(GameStateManager game)
    {

    }

    public override void ExitState(GameStateManager game)
    {
        Debug.Log("Entering State:" + stateName);
    }
}
