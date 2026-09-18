using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject point;
    public GameObject BulletPrefab;
    public float aimDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Aim(InputAction.CallbackContext ctx)
    {
        aimDir = ctx.ReadValue<float>();


        
    }
    public void Fire(){

        Instantiate(BulletPrefab, point.transform.position, point.transform.rotation);
    }
}
