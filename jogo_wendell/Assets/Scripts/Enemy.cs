using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vida;
    private GameObject player;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        vida = 10;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);
        if (vida <= 0)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            vida = vida - 10;
            Destroy(collision.gameObject);
        }
        
    }
}
