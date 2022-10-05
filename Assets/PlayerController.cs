using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    private float walkSpeed;
    private Rigidbody2D playerRB;
    
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal") * walkSpeed;
        playerRB.velocity = new Vector2(inputX, playerRB.velocity.y);
    }
}
