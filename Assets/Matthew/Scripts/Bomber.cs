using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject bob;
    public float minWaitTime;
    public float maxWaitTime;

    [SerializeField] private GameObject explosion;

    void Start()
    {
        SpawnBomber();
    }
    public void SpawnBomber()
    {
        GameObject plane = Instantiate(bob, transform.position, Quaternion.identity);
        plane.GetComponent<BomberPlane>().explosion = explosion;
        Debug.Log("BOMBER INBOUND");

        // recursive call
        Invoke("SpawnBomber", Mathf.RoundToInt(Random.Range(minWaitTime, maxWaitTime)));
    }
}
