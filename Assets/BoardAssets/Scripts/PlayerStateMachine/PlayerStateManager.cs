using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    //State info
    PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWaitState waitState = new PlayerWaitState();
    public PlayerRollState rollState = new PlayerRollState();
    public PlayerMoveState moveState = new PlayerMoveState();
    public PlayerIntersectState intersectState = new PlayerIntersectState();

    //Movement info
    public GameObject currentTile;
    public float moveTime;
    public int roll = 0, toMove = 0;

    //Player info
    public Material playerColor;
    public int playerNum;
    public int currency = 0, victoryPoints = 0; //working name
        //character model
        //name of character?
        //other stats?

    //Other info
    GameStateManager GM;

    void Start()
    {
        GM = GameObject.FindWithTag("GameController").GetComponent<GameStateManager>();
        currentState = idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    void OnChangeSelection(InputValue value)
    {
        int xVal;
        if(GM._GetCurrentStateName() == "GameRollSelectionState" && GM.playerChoosing.playerNum == playerNum && value.Get<Vector2>().x != 0)
        {
            xVal = (value.Get<Vector2>().x > 0) ? 1 : -1;
            GM.rollSelState.ChangeSelection(GM, xVal);
        }
    }
    void OnSelectDie(InputValue value)
    {
        if(GM._GetCurrentStateName() == "GameRollSelectionState" && GM.playerChoosing.playerNum == playerNum)
        {
            GM.rollSelState.ChooseDie(GM);
        }
    }

}
