using UnityEngine;

public abstract class DieBaseState
{
    public abstract void EnterState(DieStateManager die);

    public abstract void UpdateState(DieStateManager die);

    public abstract void ExitState(DieStateManager die);
}
