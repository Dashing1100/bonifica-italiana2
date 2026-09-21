using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float bulletSpeed;
    public float timer = 1f;
    public Collider2D col;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (rb != null)
        {
            rb.linearVelocity = transform.up * bulletSpeed; // Adjust the speed as needed
        }
        Destroy(gameObject, timer);

    }
}
