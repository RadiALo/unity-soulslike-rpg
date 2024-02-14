using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;
    public float dashSpeed = 24f;
    public float dashDuration = 0.4f;
    [SerializeField] private float dashCouldown = 2f;
    private float dashCouldownTimer = 0f;
    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundMask;

    public bool CanDash { get => dashCouldownTimer < 0f; }
    public bool IsGrounded { get => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask); }

    public bool IsFacingRight = true;

    #region COMPONENTS

    public Animator animator { get; private set; }
    public new Rigidbody2D rigidbody { get; set; }

    #endregion

    #region STATES

    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, "Idle");
        moveState = new PlayerMoveState(this, "Move");
        jumpState = new PlayerJumpState(this, "Jump");
        airState = new PlayerAirState(this, "Jump");
        dashState = new PlayerDashState(this, "Dash");
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.CurrentState.Update();

        dashCouldownTimer -= Time.deltaTime;
    }

    public void ReloadDash()
    {
        dashCouldownTimer = dashCouldown;
    }

    public void FlipController(float x)
    {
        if (x > 0 && !IsFacingRight)
        {
            Flip();
        } else if (x < 0 && IsFacingRight)
        {
            Flip();
        }
    }
    
    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(
            groundCheck.position,
            new Vector3(
                groundCheck.position.x,
                groundCheck.position.y - groundCheckDistance
            )
        );
        Gizmos.DrawLine(
            wallCheck.position,
            new Vector3(
                wallCheck.position.x + wallCheckDistance,
                wallCheck.position.y
            )
        );
    }
}
