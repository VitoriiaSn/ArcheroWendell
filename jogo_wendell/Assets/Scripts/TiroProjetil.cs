using UnityEngine;

public class TiroProjetil : MonoBehaviour
{
    public float forcaDoDisparo = 10f;
    private Vector2 direcaoInicial;

    void Start()
    {
        // Armazena a direção inicial ao iniciar
        direcaoInicial = GetComponent<Rigidbody2D>().velocity.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstaculoLayer"))
        {
            // Inverte apenas se colidir com objeto com a tag "obstaculoLayer"
            Reflect(collision);
        }
        else
        {
            // Destroi a bala se atingir outro tipo de objeto
            Destroy(gameObject);
        }
    }

    private void Reflect(Collision2D collision)
    {
        // Obtém a direção da normal da colisão
        Vector2 reflectionDirection = Vector2.Reflect(direcaoInicial, collision.contacts[0].normal).normalized;

        // Mantém a velocidade do tiro após a reflexão
        GetComponent<Rigidbody2D>().velocity = reflectionDirection * forcaDoDisparo;
    }
}
