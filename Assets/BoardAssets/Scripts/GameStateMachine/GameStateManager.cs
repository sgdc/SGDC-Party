using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    GameBaseState currentState;
    public GameIdleState idleState = new GameIdleState();
    public GameMinigameState minigameState = new GameMinigameState();
    public GameRollState rollState = new GameRollState();
    public GameRollSelectionState rollSelState = new GameRollSelectionState();

    public int diceNum = 4;
    [HideInInspector]
    public List<int> turnOrder;
    [HideInInspector]
    public List<int> dicePool;

    public Rigidbody dieObject;
    public Transform diceSpawnTransform; 
    [HideInInspector]
    public List<Rigidbody> dieList;


    void Start()
    {
        InitializeVariables();
        currentState = rollState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
    void InitializeVariables()
    {
        
    }
}
