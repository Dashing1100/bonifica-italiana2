using UnityEngine;

public class BomberPlane : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float duration = 3f;

    private float elapsedTime = 0f;

    void Start()
    {
        startPoint = GameObject.FindGameObjectWithTag("BombBegin").transform;
        endPoint = GameObject.FindGameObjectWithTag("BombEnd").transform;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // Normalized time (0 to 1)
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            elapsedTime += Time.deltaTime;
        }
        else
        {
            transform.position = endPoint.position; // Snap to the final position
            Destroy(gameObject);
        }
    }
}
