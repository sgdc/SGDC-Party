using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.roll > 0)
        {
            player.SwitchState(player.moveState);
        }
    }
    public override void ExitState(PlayerStateManager player)
    {

    }
}
