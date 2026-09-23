using System.Collections;
using System.Reflection.Metadata.Ecma335;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public int Hearts = 3;
    public float Iframe = 1f;

    public bool IsNoHarm;

    public float respawnTime;

    public Transform spawnPoint;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;


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
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
}
 IEnumerator NoDamage()
    {
        if (Hearts == 3)
        {
            heart3.SetActive(false);
        }
        if (Hearts == 2)
        {
            heart2.SetActive(false);
        }
        if (Hearts == 1)
        {
            heart1.SetActive(false);
        }
        Hearts--;
        IsNoHarm = true;
        yield return new WaitForSecondsRealtime(Iframe);
        IsNoHarm = false;
    }
    IEnumerator NoDamage2()
    {
        if (Hearts == 3)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
        }
        if (Hearts == 2)
        {
            heart2.SetActive(false);
            heart1.SetActive(false);
        }
        if (Hearts == 1)
        {
            heart1.SetActive(false);
        }
        Hearts--;
        Hearts--;
        IsNoHarm = true;
        yield return new WaitForSecondsRealtime(Iframe);
        IsNoHarm = false;
    }

}

