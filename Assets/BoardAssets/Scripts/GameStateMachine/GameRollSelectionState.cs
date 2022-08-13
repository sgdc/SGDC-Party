using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRollSelectionState : GameBaseState
{
    int dieIndex = 0;
    Rigidbody selectedDie;
    List<bool> selectedDice;
    int diceChosen = 1;
    

    public override void EnterState(GameStateManager game)
    {
        stateName = "GameRollSelectionState";
        Debug.Log("Entering State:" + stateName);
        //TODO: Get player in last place (Least VP's, then least currency)
        game.playerChoosing = game.playerList[game.playerStandings.Count - diceChosen]; //temp
        selectedDice = new List<bool>();
        foreach(Rigidbody _die in game.dieRBList)
        {
            selectedDice.Add(false);
        }
        ChangeSelection(game);
    }

    public override void UpdateState(GameStateManager game)
    {

    }

    public override void ExitState(GameStateManager game)
    {
        Debug.Log("Exiting State:" + stateName);
    }

    public void ChangeSelection(GameStateManager game, int increment)
    {
        Debug.Log("Changing Die Selection of player " + game.playerChoosing.playerNum + " from " + dieIndex + " by " + increment);
        do
        {
            dieIndex = (dieIndex + increment) % (game.numberOfDice);
            if(dieIndex == -1)
            {
                dieIndex = game.numberOfDice - 1;
            }
            Debug.Log("Die Selection changed to: " + dieIndex);
        } while (selectedDice[dieIndex]);
        
        selectedDie.GetComponent<Renderer>().material = selectedDie.gameObject.GetComponent<DieStateManager>().outerMaterial;
        ChangeSelection(game);        
    }

    public void ChangeSelection(GameStateManager game)
    {
        selectedDie = game.dieRBList[dieIndex];
        Debug.Log("Selected new die");
        selectedDie.GetComponent<Renderer>().material = game.playerChoosing.playerColor;
        
    }

    public void ChooseDie(GameStateManager game)
    {
        game.playerChoosing.roll = selectedDie.gameObject.GetComponent<DieStateManager>().roll;

        if(diceChosen == game.numberOfPlayers)
        {
            game.SwitchState(game.idleState);
        }
        else
        {
            diceChosen++;
            game.playerChoosing = game.playerList[game.playerStandings.Count - diceChosen];
        }


    }

}
