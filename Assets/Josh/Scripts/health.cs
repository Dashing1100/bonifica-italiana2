using System.Collections;
using System.Reflection.Metadata.Ecma335;
using Unity.Cinemachine;
using UnityEngine;

public class health : MonoBehaviour
{
    public int Hearts = 3;
    public float Iframe = 1f;

    public bool IsNoHarm;

    public float respawnTime;

    public Transform spawnPoint;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet")|| other.collider.CompareTag("Enemy"))
        {
            if (!IsNoHarm)
            {
                StartCoroutine(NoDamage());
            }
            else
            {
                return;
            }
        }
        if (other.collider.CompareTag("bomb"))
        {
            if (!IsNoHarm)
            {
                StartCoroutine(NoDamage2());
            }
            else
            {
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        if(Hearts <= 0)
        {
            
            StartCoroutine(RespawnRoutine());
            Hearts = 3;

        }
    }
    IEnumerator RespawnRoutine()
{
  yield return new WaitForSecondsRealtime(respawnTime);
        Hearts = 3;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
}
 IEnumerator NoDamage()
    {
     Hearts--;
        IsNoHarm = true;
        yield return new WaitForSecondsRealtime(Iframe);
        IsNoHarm = false;
    }
    IEnumerator NoDamage2()
    {
        Hearts--;
        Hearts--;
        IsNoHarm = true;
        yield return new WaitForSecondsRealtime(Iframe);
        IsNoHarm = false;
    }

}

