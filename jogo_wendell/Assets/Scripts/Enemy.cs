using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vidaInimigo;
    private GameObject player;
    public GameObject particle;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float distanciaParaParar = 2.0f;
    public float rotacaoSpeed = 5.0f;
    public LayerMask obstaculoLayer;

    private float tempoParaProximoTiro = 0f;
    public float intervaloDeTiro = 2f;

    void Start()
    {
        vidaInimigo = 1;
        player = GameObject.Find("Player");
    }
    

    void Update()
    {
        float distancia = Vector2.Distance(transform.position, player.transform.position);

        if (distancia > distanciaParaParar)
        {
            RotateTowardsPlayer();
            MoveTowardsPlayer();
        }

        if (vidaInimigo <= 0)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        tempoParaProximoTiro -= Time.deltaTime;
        if (tempoParaProximoTiro <= 0f)
        {
            Shoot();
            tempoParaProximoTiro = intervaloDeTiro;
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotacaoSpeed * Time.deltaTime);
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanciaParaParar, obstaculoLayer);

        if (hit.collider == null)
        {
            transform.Translate(direction * 2f * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        // Instancia uma bala no ponto de disparo
        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            vidaInimigo--;
            Destroy(collision.gameObject);
        }
    }
}
