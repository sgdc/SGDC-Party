using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int playerNum;
    public int currency = 0, victoryPoints = 0; //working name
        //character model
        //name of character?
        //other stats?


    void Start()
    {
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

}
