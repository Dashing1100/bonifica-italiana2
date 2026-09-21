using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float bulletSpeed;
    private float timer = 0f;
    public Collider2D col;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        timer = 0.1f;

        if (rb != null)
        {
            rb.linearVelocity = -transform.right * bulletSpeed; // Adjust the speed as needed
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f) // Destroy the bullet after 5 seconds to prevent memory leaks
        {
            Destroy(gameObject);
        }

    }

}
