using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    Vector3 faceNegative = new Vector3(-1, 1, 1);
    Vector3 facePositive = Vector3.one;
    [SerializeField] private Joystick joystick;

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
        Vector2 direction = GetSetDirection();
    }

    private void Hola()
    {
        throw new NotImplementedException();
    }

    private void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float inputXJoystick = joystick.Horizontal * moveSpeed;
        playerRB.velocity = new Vector2(inputXJoystick, playerRB.velocity.y);
        //playerRB.velocity = new Vector2(inputX, playerRB.velocity.y);
        basicPlayerAnimator.SetFloat(IdSpeed, Math.Abs(inputXJoystick));
    }

    private Vector2 GetSetDirection()
    {
        if (playerRB.velocity.x < 0) transform.localScale = faceNegative;
        else if (playerRB.velocity.x > 0) transform.localScale = facePositive;
        Move();
        return transform.localScale;
    }
}
