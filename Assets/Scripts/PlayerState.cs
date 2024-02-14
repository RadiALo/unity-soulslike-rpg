using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    
    protected string animatorBoolName;

    protected float xInput;

    public PlayerState(Player player, string animatorBoolName)
    {
        this.player = player;
        this.animatorBoolName = animatorBoolName;
    }

    private void ToggleAnimator(bool toggle)
    {
        player.animator.SetBool(animatorBoolName, toggle);
    }

    public virtual void Enter() {
        ToggleAnimator(true);
    }

    public virtual void Update() {
        xInput = Input.GetAxis("Horizontal");
        player.FlipController(xInput);
        player.animator.SetFloat("yVelocity", player.rigidbody.velocity.y);
    }

    public virtual void Exit()
    {
        ToggleAnimator(false);
    }
}
