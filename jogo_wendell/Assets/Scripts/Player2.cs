using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
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
        public float tempoEntreTiros = 0.1f;
        

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        MovePlayer();

        //disparo = Input.GetKey(KeyCode.P);
        Atirar();
    }       

    private void Atirar()
    {
        if (Input.GetKeyDown(KeyCode.P) && podeAtirar)
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
            //chamar game overd
        }
    }
    void RotatePlayer()
    {
        float rotationInput = 0;

        if (Input.GetKey("a"))
        {
            rotationInput = 1;
        }
        else if (Input.GetKey("d"))
        {
            rotationInput = -1;
        }

        transform.Rotate(0, 0, -rotationInput * rotationSpeed * Time.deltaTime);
    }


    void MovePlayer()
    {
        float verticalInput = 0;

        if (Input.GetKey("w"))
        {
            verticalInput = 1;
        }
        else if (Input.GetKey("s"))
        {
            verticalInput = -1;
        }

        transform.Translate(0, verticalInput * movementSpeed * Time.deltaTime, 0);
    }

    
}

