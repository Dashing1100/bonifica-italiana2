using UnityEngine;

public class BomberPlane : MonoBehaviour
{
    public float duration = 3f;

    public float speed = 10f;

    private float elapsedTime = 0f;
    public GameObject bomb;
    public float minWaitTime;
    public float maxWaitTime;

    private Rigidbody2D rb;

    public GameObject explosion;

    void Start()
    {
        SpawnBomb();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityX = -speed;

        Destroy(gameObject, duration);
    }


    public void SpawnBomb()
    {
        GameObject nuke = Instantiate(bomb, transform.position, Quaternion.identity);
        nuke.GetComponent<bomb>().explosion = explosion;
        Debug.Log("BOMB INBOUND");

        // recursive call
        Invoke("SpawnBomb", Random.Range(minWaitTime, maxWaitTime));
    }
}
