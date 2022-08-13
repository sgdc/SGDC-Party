using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRollSelectionState : GameBaseState
{
    public override void EnterState(GameStateManager game)
    {
        stateName = "GameRollSelectionState";
        Debug.Log("Entering State:" + stateName);
        //TODO: Get player in last place (Least VP's, then least currency)
        game.playerChoosing = 1; //temp
        
    }

    public override void UpdateState(GameStateManager game)
    {

    }

    public override void ExitState(GameStateManager game)
    {
        Debug.Log("Exiting State:" + stateName);
    }
}
