using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D playerRB;
    private GameObject basicPlayer;
    private Transform basicPlayerTransform;
    private Animator basicPlayerAnimator;
    private int IdSpeed;
    private bool isFlippedInX;
    private bool isGrounded;
    private float isGroundedRange;
    private LayerMask selectedLayerMask;
    private Transform checkGroundPoint;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        basicPlayer = GameObject.Find("BasicPlayer");
        basicPlayerTransform = basicPlayer.GetComponent<Transform>();
        basicPlayerAnimator = basicPlayer.GetComponent<Animator>();
        IdSpeed = Animator.StringToHash("speed");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckAndSetDirection();
    }

    private void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        playerRB.velocity = new Vector2(inputX, playerRB.velocity.y);
        basicPlayerAnimator.SetFloat(IdSpeed, Math.Abs(inputX));
    }

    private void CheckAndSetDirection()
    {
        if (playerRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFlippedInX = true;
        }
        else if (playerRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            isFlippedInX = false;
        }
    }

    private void Jump()
    {
        isGrounded = Physics2D.Raycast(checkGroundPoint.position, Vector2.down, isGroundedRange, selectedLayerMask);
        if (Input.GetButtonDown("Jump") && (isGrounded || (canDoubleJump && playerExtrasTracker.CanDoubleJump)))
        {
            if (isGrounded)
            {
                canDoubleJump = true;
                Instantiate(dustJump, transformDustPoint.position, Quaternion.identity);
            }
            else
            {
                canDoubleJump = false;
                animatorStandingPlayer.SetTrigger(IdCanDoubleJump);
            }
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }
        animatorStandingPlayer.SetBool(IdIsGrounded, isGrounded);
    }
}
