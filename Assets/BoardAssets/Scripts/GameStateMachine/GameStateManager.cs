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

    public int numberOfDice = 4;
    [HideInInspector]
    public List<int> turnOrder;
    [HideInInspector]
    public List<int> dicePool;

    public Rigidbody dieObject;
    public Transform diceSpawnTransform; 
    [HideInInspector]
    public List<Rigidbody> dieRBList;

    public GameObject _camControllerOBJ;
    [HideInInspector]
    public CameraStateManager camController;

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
        Debug.Log("Entering State");
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
        Debug.Log("State Entered");
    }
    void InitializeVariables()
    {
        Debug.Log("Game State Manager: Init Vars Start");
        camController = _camControllerOBJ.GetComponent<CameraStateManager>();
        Debug.Log("Init Vars Done");
    }
}
