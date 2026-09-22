using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject bob;
    public float minWaitTime;
    public float maxWaitTime;

    private float time;

    void Start()
    {
        SpawnBomber();
    }
    public void SpawnBomber()
    {
        GameObject plane = Instantiate(bob, transform.position, Quaternion.identity);
        Debug.Log("BOMBER INBOUND");

        // recursive call
        Invoke("SpawnBomber", Mathf.RoundToInt(Random.Range(minWaitTime, maxWaitTime)));
    }
}
