using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int vidaPlayer = 1;
    
    public float rotationSpeed = 100f;
    public float movementSpeed = 5f;
    public GameObject tiroProjetil;
    public Transform arma;
    private bool disparo;
    public float forcaDoDisparo;
    private bool flipX = false;
    public bool podeAtirar = true;
    public float tempoEntreTiros = 0.2f;
    
    void Update()
    {
        RotatePlayer();
        MovePlayer();

        disparo = Input.GetKey(KeyCode.E);
        Atirar();
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
    
    public void Damage(int dmg)
    {
        vidaPlayer -= dmg; 
        if( vidaPlayer <= 0)
        {
            // chamar game over
        }
    }
    void RotatePlayer()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, -rotationInput * rotationSpeed * Time.deltaTime);
    }

    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(0, verticalInput * movementSpeed * Time.deltaTime, 0);
    }
}