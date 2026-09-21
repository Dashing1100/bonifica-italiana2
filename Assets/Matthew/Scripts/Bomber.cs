using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject bob;
    public float minWaitTime;
    public float maxWaitTime;

    void Start()
    {
        InvokeRepeating("SpawnBomber", Mathf.RoundToInt(Random.Range(minWaitTime, maxWaitTime)), 0f);
    }

    public void SpawnBomber()
    {
        GameObject plane = Instantiate(bob, transform.position, Quaternion.identity);
    }
}
