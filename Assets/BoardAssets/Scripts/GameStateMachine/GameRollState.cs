using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRollState : GameBaseState
{

    int _diceNum = 4;
    float _maxRollInterval = 0.5f;
    float _rollInterval;

    float _countDown;
    float _maxCountDown = 1.5f;
    float _finalCountDown = 10f;

    Rigidbody _newDie;

    //Class Functions

    public override void EnterState(GameStateManager game)
    {
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
       _GetDiceValues(game);
    }


    //Private Functions


    void _RollDie(GameStateManager game)
    {
        _newDie = Rigidbody.Instantiate(game.dieObject,game.diceSpawnTransform);
        game.dieList.Add(_newDie);
        _diceNum--;
    }

    bool _CheckAllDiceStopped(GameStateManager game)
    {
        foreach (Rigidbody _die in game.dieList)
        {
            if (_die.velocity != Vector3.zero || _die.angularVelocity != Vector3.zero)
                return false;
        }
        return true;
    }

    void _RollAdditionalDice(GameStateManager game)
    {
        _rollInterval -= Time.deltaTime;

        if (_diceNum > 0 && _rollInterval <= 0)
        {
            _RollDie(game);
            _rollInterval = _maxRollInterval;
        }
    }

    void _InitializeVariables(GameStateManager game)
    {
        _rollInterval = _maxRollInterval;
        game.dieList = new List<Rigidbody>();
        game.dicePool = new List<int>();
        _countDown = _maxCountDown;
    }

    void _GetDiceValues(GameStateManager game)
    {
        foreach (Rigidbody _die in game.dieList)
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
}
