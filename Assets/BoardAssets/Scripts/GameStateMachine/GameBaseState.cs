using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState(GameStateManager player);

    public abstract void UpdateState(GameStateManager player);

    public abstract void ExitState(GameStateManager player);
}
