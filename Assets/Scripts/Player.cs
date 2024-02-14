using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;
    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundMask;

    public bool IsGrounded { get => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask); }

    private bool isFacingRight = true;

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

    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, "Idle");
        moveState = new PlayerMoveState(this, "Move");
        jumpState = new PlayerJumpState(this, "Jump");
        airState = new PlayerAirState(this, "Jump");
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
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float x)
    {
        if (x > 0 && !isFacingRight)
        {
            Flip();
        } else if (x < 0 && isFacingRight)
        {
            Flip();
        }
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
