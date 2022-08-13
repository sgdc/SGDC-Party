using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    //State info
    GameBaseState currentState;
    public GameIdleState idleState = new GameIdleState();
    public GameMinigameState minigameState = new GameMinigameState();
    public GameRollState rollState = new GameRollState();
    public GameRollSelectionState rollSelState = new GameRollSelectionState();

    //Player and Turn info
    public int numberOfPlayers;
    public List<Material> playerMaterialList;
    [HideInInspector] public List<int> turnOrder;
    public List<PlayerStateManager> playerList;
    public List<int> playerStandings;
    public int currentPlayerTurn;

    //Dice info
    public int numberOfDice = 4;
    public Rigidbody dieObject;
    public Transform diceSpawnTransform; 
    public List<Rigidbody> dieRBList;
    [HideInInspector] public List<int> dicePool;

    public PlayerStateManager playerChoosing;
    public GameObject DM;

    //Camera info
    public GameObject _camControllerOBJ;
    [HideInInspector] public CameraStateManager camController;

    

    void Start()
    {
        InitializeVariables();
        currentState = rollState;
        currentState.EnterState(this);

        numberOfPlayers = playerList.Count;
        playerStandings = new List<int>();
        for(int i = 0; i < numberOfPlayers; i++)
        {
            playerStandings.Add(i+1);
        }
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        Debug.Log("Beginning to switch from " + currentState.stateName + " to " + state.stateName + "...");
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
        Debug.Log("State successfully switched");
    }

    void InitializeVariables()
    {
        Debug.Log("Game State Manager: Init Vars Start");
        camController = _camControllerOBJ.GetComponent<CameraStateManager>();
        Debug.Log("Init Vars Done");
    }

    public string _GetCurrentStateName()
    {
        return currentState.stateName;
    }

}
