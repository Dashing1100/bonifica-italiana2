
using JetBrains.Annotations;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;


public class LandEnemy : MonoBehaviour
{
    [SerializeField] private int health = 1;

    private enum EnemyState {patrol, resting, charging}

    [Header("SHOOTER")]
    public GameObject projectile;
    public Transform bulletPos;
    private float timer;
    public int fireRate;
    private GameObject player;


    [Header("CHARGER")]
    private EnemyState currentState = EnemyState.patrol;
    [SerializeField] private Transform castPoint;
   [SerializeField] private float windUpTime = 1f;

   
    public float detectionRange = 10;

    public bool isCharging;
    public bool isShooter;
    public bool isCharger;

    [Header("MOVEMENT SETTINGS")]
    public float speed;
    public float maxSlopeAngle = 35f;
    public float wallCheckDistance = 0.5f;
    private int direction = 1; 

    private Vector2 slopeNormalPerpendicular;
    private float slopeAngle;
    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;

    [SerializeField] private LayerMask playerLayer;

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
  
    private void FixedUpdate()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        CheckEnvironment();
        if (isShooter)
        {
            Vector3 currentLocalEuler = transform.localEulerAngles;
            transform.localRotation = Quaternion.Euler(currentLocalEuler.x, currentLocalEuler.y, 0f);
            if (player == null) return;
            float distance = Vector2.Distance(transform.position, player.transform.position);


            
                if (distance < 10)
                {
                    if (player == null) return;

                    bool playerIsToTheRight = player.transform.position.x > transform.position.x;

                    if (playerIsToTheRight != movingRight)
                    {
                        Flip();
                    }
                    rb.constraints = RigidbodyConstraints2D.FreezePosition;
                    timer += Time.fixedDeltaTime;

                    if (timer > fireRate)
                    {
                        timer = 0;
                        Shoot();
                    }
                }
            
            else
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }


        }
        if (isCharger )
        {
            if (currentState == EnemyState.patrol)
            {
                speed = 8;
            }
            else
           

            if (currentState == EnemyState.charging)
            {
                speed = 16;
            }
            
            if (currentState == EnemyState.resting)
            {
                speed = 0;
            }
            if (player == null) return;
            float distance = Vector2.Distance(transform.position, player.transform.position);
            
                if (distance < 10 && !isCharging)
                {
                    if (player == null) return;
                
                    StartCoroutine(PerformChargeSequence());

                }
           
            
            rb.linearVelocity = new Vector2((movingRight ? 1 : -1) * speed, -9.8f);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f, groundLayer);

            if (groundInfo.collider == false)
            {
                Flip();
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground"))
        {
            Flip();
        }

        if (collision.collider.CompareTag("Player"))
        {
            Vector2 enemyPos = collision.transform.position;


        }
        if (collision.collider.CompareTag("PBullet"))
        {
            health--;

        }
    }
    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    void Shoot()
    {
        Instantiate(projectile, bulletPos.position, Quaternion.identity);
    }

    void CheckEnvironment()
    {
       
                                   
    RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f, groundLayer);

        if (groundCheck)
        {
            isGrounded = true;
            slopeAngle = Vector2.Angle(groundInfo.normal, Vector2.up);
            
            slopeNormalPerpendicular = Vector2.Perpendicular(groundInfo.normal).normalized;
        }
        else
        {
            isGrounded = false;
            slopeAngle = 0;
        }

        
        Vector2 checkDirection = new Vector2(direction, 0);
        if (isGrounded && slopeAngle > 0 && slopeAngle <= maxSlopeAngle)
        {
          
            checkDirection = -slopeNormalPerpendicular * direction;
        }

        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, checkDirection, wallCheckDistance, groundLayer);

       
        if ((wallHit && Mathf.Abs(Vector2.Angle(wallHit.normal, Vector2.up) - 90f) < 5f))
        {
            Flip();
        }
    }
    IEnumerator PerformChargeSequence()
    {
        
      currentState = EnemyState.resting;

        
yield return new WaitForSeconds(windUpTime);

         isCharging = true;
        currentState = EnemyState.charging;
        if (currentState == EnemyState.charging)
        {
            Debug.Log("charging");
        }
        
        float chargeDuration = 1f;
        yield return new WaitForSeconds(chargeDuration);

        
        currentState = EnemyState.resting;
        rb.linearVelocity = Vector2.zero; 

        yield return new WaitForSeconds(chargeDuration);

        
        currentState = EnemyState.patrol;

        isCharging = false;

    }
   
}
