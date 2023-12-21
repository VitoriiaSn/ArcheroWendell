using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2D;
    private Vector2 PlayerDirection;
    public Animator anim;
    public float speed;
    public int vida;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Atualizar parâmetros do Animator
        anim.SetFloat("Horizontal", PlayerDirection.x);
        anim.SetFloat("Vertical", PlayerDirection.y);
        anim.SetFloat("Speed", PlayerDirection.magnitude);
    }

    void FixedUpdate()
    {
        playerRigidbody2D.MovePosition(playerRigidbody2D.position + PlayerDirection * speed * Time.fixedDeltaTime);
    }
}