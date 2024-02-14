using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashTimer;

    public PlayerDashState(Player player, string animatorBoolName) : base(player, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.ReloadDash();
        dashTimer = player.dashDuration;
    }

    public override void Update()
    {
        base.Update();

        dashTimer -= Time.deltaTime;

        player.rigidbody.velocity = new Vector2(
            player.dashSpeed * (player.IsFacingRight ? 1 : -1),
            0
        );

        if (dashTimer < 0)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.rigidbody.velocity = new Vector2(0, player.rigidbody.velocity.y);
    }
}
