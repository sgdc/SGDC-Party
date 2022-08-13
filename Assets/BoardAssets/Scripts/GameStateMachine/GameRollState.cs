using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRollState : GameBaseState
{
    int _diceRemaining;
    float _maxRollInterval = 0.5f;
    float _rollInterval;

    float _countDown;
    float _maxCountDown = 1.5f;
    float _finalCountDown = 10f;

    Rigidbody _newDie;

    //Class Functions

    public override void EnterState(GameStateManager game)
    {
        stateName = "GameRollState";
        Debug.Log("Entering State: " + stateName);
        _InitializeVariables(game);
        _RollDie(game);
    }

    public override void UpdateState(GameStateManager game)
    {
        _RollAdditionalDice(game);
        _HandleCountdowns(game);
    }

    public override void ExitState(GameStateManager game)
    {
        Debug.Log("Exiting State: " + stateName);
        _GetDiceValues(game);
        game.camController.TransitionCam(game.camController.rollSelectCam);
        _SetDiceStates(game);
    }


    //Private Functions


    void _RollDie(GameStateManager game)
    {
        _newDie = GameObject.Instantiate(game.dieObject,game.diceSpawnTransform);
        game.dieRBList.Add(_newDie);
        _newDie.GetComponent<DieStateManager>().ID = game.numberOfDice - _diceRemaining;
        _newDie.GetComponent<DieStateManager>().rollSelectCam = game.camController.rollSelectCam;
        _newDie.GetComponent<DieStateManager>().numDice = game.numberOfDice;
        _diceRemaining--;
    }

    bool _CheckAllDiceStopped(GameStateManager game)
    {
        foreach (Rigidbody _die in game.dieRBList)
        {
            if (_die.velocity != Vector3.zero || _die.angularVelocity != Vector3.zero)
                return false;
        }
        return true;
    }

    void _RollAdditionalDice(GameStateManager game)
    {
        _rollInterval -= Time.deltaTime;

        if (_diceRemaining > 0 && _rollInterval <= 0)
        {
            _RollDie(game);
            _rollInterval = _maxRollInterval;
        }
    }

    void _InitializeVariables(GameStateManager game)
    {
        _rollInterval = _maxRollInterval;
        game.dieRBList = new List<Rigidbody>();
        game.dicePool = new List<int>();
        _countDown = _maxCountDown;
        _diceRemaining = game.numberOfDice;
    }

    void _GetDiceValues(GameStateManager game)
    {
        foreach (Rigidbody _die in game.dieRBList)
        {
            game.dicePool.Add(_die.GetComponent<DieStateManager>().GetSideFaceUp());
        }
    }

    void _HandleCountdowns(GameStateManager game)
    {
        _finalCountDown -= Time.deltaTime;
        if (_CheckAllDiceStopped(game))
            _countDown -= Time.deltaTime;
        else
            _countDown = _maxCountDown;

        if (_countDown <= 0 || _maxCountDown <= 0)
            game.SwitchState(game.idleState);
    }

    void _SetDiceStates(GameStateManager game)
    {
        foreach (Rigidbody _die in game.dieRBList)
        {
            _die.GetComponent<DieStateManager>().SwitchState(_die.GetComponent<DieStateManager>().moveState);
        }
        game.SwitchState(game.rollSelState);
    }
}
