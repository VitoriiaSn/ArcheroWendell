
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float movementSpeed = 5f;
    public GameObject tiroProjetil;
    public Transform arma;
    private bool disparo;
    public float forcaDoDisparo;
    private bool flipX = false;
    
    void Update()
    {
        RotatePlayer();
        MovePlayer();

        disparo = Input.GetKey(KeyCode.E);
        Atirar();
    }

    private void Atirar()
    {
        if (disparo == true)
        {
            GameObject temp = Instantiate(tiroProjetil);
            temp.transform.position = arma.position;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaDoDisparo, 0);
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
