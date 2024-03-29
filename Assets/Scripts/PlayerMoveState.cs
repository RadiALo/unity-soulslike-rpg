using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, string animatorBoolName) : base(player, animatorBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (xInput == 0)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
