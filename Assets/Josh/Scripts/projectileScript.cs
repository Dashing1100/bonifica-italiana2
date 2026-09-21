using UnityEngine;
using System;


public class projectileScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;

    public float lifetime = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation =Quaternion.Euler(0,0,rot +90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
