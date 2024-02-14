using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, string animatorBoolName) : base(player, animatorBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
        {
            player.stateMachine.ChangeState(player.moveState);
        }
    }
}
