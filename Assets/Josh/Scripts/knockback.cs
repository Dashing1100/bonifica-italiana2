using UnityEngine;
using JetBrains.Annotations;
using System.Collections;
using System.Runtime.CompilerServices;

public class knockback : MonoBehaviour
{
   


    private GameObject player;

    [SerializeField] private float knockbackTime = 0.2f;
    public bool IsGettingKnockedBack { get; private set; }

    [SerializeField] private LayerMask playerLayer;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Enemy"))
        {
            Vector2 enemyPos = collision.transform.position;


            float knockbackPower = 10f;


            ApplyKnockback(enemyPos, knockbackPower);
        }
    }
    public void ApplyKnockback(Vector2 damageSourcePosition, float strength)
    {
        if (IsGettingKnockedBack) return;


        Vector2 direction = ((Vector2)transform.position - damageSourcePosition).normalized;

        StartCoroutine(KnockbackRoutine(direction, strength));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction, float strength)
    {
        IsGettingKnockedBack = true;


        rb.linearVelocity = Vector2.zero;


        rb.AddForce(direction * strength, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        IsGettingKnockedBack = false;
    }
}




