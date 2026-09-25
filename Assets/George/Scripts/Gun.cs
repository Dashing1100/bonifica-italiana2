using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject point;
    public GameObject BulletPrefab;
    public float aimDir;
    public float timer;
    public float cooldown;
    public AudioClip GunFire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }
    public void Aim(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() != 0)
        {
            aimDir = ctx.ReadValue<float>();
            point.transform.localPosition = new Vector3(0.6f * (aimDir), -0.2f, 0);
            //point.transform.rotation = Quaternion.Euler(0, 0, -180 * (aimDir));
            if (aimDir < 0)
            {
                point.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                point.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
    public void Fire(){
        
        if (timer <= 0)
        {
            AudioSource.PlayClipAtPoint(GunFire, transform.position);
            GameObject Bullet = Instantiate(BulletPrefab, point.transform.position, Quaternion.Euler(0,0,90)); 
            Bullet.GetComponent<Rigidbody2D>().linearVelocity = transform.right * Bullet.GetComponent<Bullet>().bulletSpeed * aimDir;
            timer = cooldown;
        }
    }
}
