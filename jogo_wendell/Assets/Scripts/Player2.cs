using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public int vidaPlayer = 1;
    
    public float rotationSpeed = 100f;
    public float movementSpeed = 5f;
    public GameObject tiroProjetil;
    public Transform arma;
    public Animator animP1;
    private bool disparo;
    public float forcaDoDisparo;
    private bool flipX = false;
    public bool podeAtirar = true;
    public float tempoEntreTiros = 0.1f;


    private void Start()
    {
        animP1 = GetComponent<Animator>();
    }

    void Update()
    {
        RotatePlayer();
        MovePlayer();

        disparo = Input.GetKey(KeyCode.E);
        Atirar();
        if( vidaPlayer <= 0)
        {
            animP1.SetTrigger("Die");
            tempoEntreTiros = 10000000000000000000000000000f;
            movementSpeed = 0f;
            rotationSpeed = 0f;
            
        }
    }       

    private void Atirar()
    {
        if (disparo && podeAtirar)
        {
            GameObject temp = Instantiate(tiroProjetil);
            temp.transform.position = arma.position;
            Rigidbody2D tempRb = temp.GetComponent<Rigidbody2D>();
            Vector2 direcaoDoTiro = transform.up;  
            tempRb.velocity = direcaoDoTiro * forcaDoDisparo;
            
            Destroy(temp.gameObject, 3f);
            
            podeAtirar = false;
            StartCoroutine(Recarregar());
        }
    }

    private IEnumerator Recarregar()
    {
        yield return new WaitForSeconds(tempoEntreTiros);
        podeAtirar = true;
    }
    
    public void Damage1(int dmg)
    {
        vidaPlayer -= dmg; 
        if( vidaPlayer <= 0)
        {
            animP1.SetTrigger("Die");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("bullet"))
        {
            vidaPlayer -= 1;
        }
    }

    void RotatePlayer()
    {
        float rotationInput = 0;

        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationInput = -1;
        }

        transform.Rotate(0, 0, -rotationInput * rotationSpeed * Time.deltaTime);
    }


    void MovePlayer()
    {
        float verticalInput = 0;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1;
        }

        transform.Translate(0, verticalInput * movementSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            vidaPlayer -= 1;
        }
    }
}