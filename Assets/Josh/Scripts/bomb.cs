using UnityEngine;

public class bomb : MonoBehaviour
{

    public Rigidbody2D rb;
    public CircleCollider2D CC2D;
    SpriteRenderer sR;
    void Start()
    {
        Destroy(gameObject, 5f);
        rb = GetComponent<Rigidbody2D>();
        CC2D = GetComponent<CircleCollider2D>();
        sR = GetComponent<SpriteRenderer>();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Player"))
        {
            CC2D.radius = 1.25f;
            sR.enabled = false;
            Destroy(gameObject, .1f);
        }
    }
}
