using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, string animatorBoolName) : base(player, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.rigidbody.velocity = new Vector2(
            player.rigidbody.velocity.x,
            player.jumpForce
        );
    }

    public override void Update()
    {
        base.Update();

        if (player.rigidbody.velocity.y < 0)
        {
            player.stateMachine.ChangeState(player.airState);
        }
    }
}
