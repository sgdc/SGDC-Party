using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

    PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWaitState waitState = new PlayerWaitState();
    public PlayerRollState rollState = new PlayerRollState();
    public PlayerMoveState moveState = new PlayerMoveState();
    public PlayerIntersectState intersectState = new PlayerIntersectState();

    public GameObject currentTile;

    public float moveTime;

    public int roll = 0;
    public int toMove = 0;

    

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
