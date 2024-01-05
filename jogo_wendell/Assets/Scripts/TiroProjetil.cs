using UnityEngine;

public class TiroProjetil : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") || collision.CompareTag("Player2") || collision.CompareTag("obstaculoLayer"))
        {
            Destroy(gameObject);
        }
    }
}