using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, string animatorBoolName) : base(player, animatorBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGrounded)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
