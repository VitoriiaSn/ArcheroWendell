using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2D;
    private Vector2 PlayerDirection;
    public Animator anim;
    public float speed;
    public int vida;

    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        PlayerDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        anim.SetFloat("Horizontal", PlayerDirection.x);
        anim.SetFloat("Vertical", PlayerDirection.y);
        anim.SetFloat("Speed", PlayerDirection.magnitude);
    }
    

    void FixedUpdate()
    {
        playerRigidbody2D.MovePosition(playerRigidbody2D.position + PlayerDirection * speed * Time.fixedDeltaTime);
    }
}