using UnityEngine;

public abstract class GameBaseState
{
    public string stateName;

    public abstract void EnterState(GameStateManager game);

    public abstract void UpdateState(GameStateManager game);

    public abstract void ExitState(GameStateManager game);
}
