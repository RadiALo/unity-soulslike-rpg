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

        player.rigidbody.velocity = new Vector2(
            xInput * player.moveSpeed,
            player.rigidbody.velocity.y
        );

        if (player.IsGrounded)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
