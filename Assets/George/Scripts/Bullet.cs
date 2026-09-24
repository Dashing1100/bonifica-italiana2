using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float bulletSpeed = 40;
    public float timer = 1f;
    public Collider2D col;
    public float dir;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (rb != null)
        {
            //rb.linearVelocity = transform.right * bulletSpeed * dir; // Adjust the speed as needed
        }
        Destroy(gameObject, timer); 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject, 0.01f);
        }
    }
}
