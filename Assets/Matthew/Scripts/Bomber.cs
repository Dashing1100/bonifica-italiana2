using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject bob;

    void Start()
    {
        SpawnBomber();
    }

    public void SpawnBomber()
    {
        GameObject plane = Instantiate(bob, transform.position, Quaternion.identity);
    }
}
