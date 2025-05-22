using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D boxcol;

    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private LayerMask groundLayer;

    private int maxJumps = 1;
    private int extraJumps = 1;

    private bool isPunching = false;
    private bool isKicking = false;
    private bool isCrouchKicking = false;
    private bool isFlyingKicking = false;

    public bool isAttacking = false;

    enum MovementState { Idle, Walk, Jump, Fall, Crouch, Punch, Kick, Crouch_Kick, Flying_Kick, Hurt }
    MovementState state = MovementState.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxcol = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Move();
        UpdateAnimationState();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            extraJumps--;
            Jump();
        }

        if (IsGrounded())
        {
            extraJumps = maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.F) && IsGrounded() && !isPunching)
        {
            StartCoroutine(PunchRoutine());
        }
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl) && IsGrounded() && !isCrouchKicking)
        {
            StartCoroutine(CrouchKickRoutine());
        }
        else if (Input.GetKeyDown(KeyCode.R) && IsGrounded() && !isKicking)
        {
            StartCoroutine(KickRoutine());
        }
        else if (Input.GetKeyDown(KeyCode.R) && !IsGrounded() && !isFlyingKicking)
        {
            StartCoroutine(FlyingKickRoutine());
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    IEnumerator PunchRoutine()
    {
        isPunching = true;
        isAttacking = true;

        state = MovementState.Punch;
        anim.SetInteger("State", (int)state);

        yield return new WaitForSeconds(0.45f);

        isPunching = false;
        isAttacking = false;
    }

    IEnumerator KickRoutine()
    {
        isKicking = true;
        isAttacking = true;

        state = MovementState.Kick;
        anim.SetInteger("State", (int)state);

        yield return new WaitForSeconds(0.7f);

        isKicking = false;
        isAttacking = false;
    }

    IEnumerator CrouchKickRoutine()
    {
        isCrouchKicking = true;
        isAttacking = true;

        state = MovementState.Crouch_Kick;
        anim.SetInteger("State", (int)state);

        yield return new WaitForSeconds(0.85f);

        isCrouchKicking = false;
        isAttacking = false;
    }

    IEnumerator FlyingKickRoutine()
    {
        isFlyingKicking = true;
        isAttacking = true;

        state = MovementState.Flying_Kick;
        anim.SetInteger("State", (int)state);

        yield return new WaitUntil(() => IsGrounded());

        isFlyingKicking = false;
        isAttacking = false;
    }

    void UpdateAnimationState()
    {
        if (isFlyingKicking || isPunching || isKicking || isCrouchKicking)
            return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            state = MovementState.Crouch;
        }
        else if (rb.velocity.y > 0.1f)
        {
            state = MovementState.Jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.Fall;
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            state = MovementState.Walk;
        }
        else
        {
            state = MovementState.Idle;
        }

        anim.SetInteger("State", (int)state);

        if (state == MovementState.Walk)
        {
            sprite.flipX = rb.velocity.x < 0;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 7f);
    }

    bool IsGrounded()
    {
        float extraHeight = 0.8f;
        return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.down, extraHeight, groundLayer);
    }
}
