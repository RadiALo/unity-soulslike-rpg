using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, string animatorBoolName) : base(player, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.rigidbody.velocity = new Vector2(
            xInput * player.moveSpeed,
            player.rigidbody.velocity.y
        );

        if (!player.IsGrounded)
        {
            player.stateMachine.ChangeState(player.airState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
        {
            player.stateMachine.ChangeState(player.jumpState);
        }
    }
}
