using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;
    [SerializeField] private int moveSpeed = 5;
    BoxCollider2D boxcol;
    [SerializeField] LayerMask groundLayer;
    private int maxJumps = 1;
    private int extraJumps = 1;
    private bool isPunching = false;
    private bool isKicking = false;

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
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    IEnumerator PunchRoutine()
    {
        isPunching = true;
        state = MovementState.Punch;
        anim.SetInteger("State", (int)state);
        yield return new WaitForSeconds(0.3f);
        isPunching = false;
    }

    IEnumerator KickRoutine()
    {
        isKicking = true;
        state = MovementState.Kick;
        anim.SetInteger("State", (int)state);
        yield return new WaitForSeconds(0.3f);
        isKicking = false;
    }

    void UpdateAnimationState()
    {
        if (isPunching) return;

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R))
        {
            state = MovementState.Crouch_Kick;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            state = MovementState.Crouch;
        }
        else if (Input.GetKey(KeyCode.R) && !IsGrounded())
        {
            state = MovementState.Flying_Kick;
        }
        else if (Input.GetKey(KeyCode.R))
        {
            state = MovementState.Kick;
        }
        else if (rb.velocity.y > 0.1f)
        {
            state = MovementState.Jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.Fall;
        }
        else if (rb.velocity.x != 0)
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

        Debug.Log("Current State: " + state);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 7f);
    }

    bool IsGrounded()
    {
        float extraHeight = 0.8f;
        return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0, Vector2.down, extraHeight, groundLayer);
    }
}