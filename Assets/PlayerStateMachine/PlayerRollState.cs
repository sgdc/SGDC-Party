using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        player.roll = Random.Range(1, 6);
        player.SwitchState(player.moveState);
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }
    public override void ExitState(PlayerStateManager player)
    {

    }
}
