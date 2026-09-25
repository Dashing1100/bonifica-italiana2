using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Where projectiles spawn from")]
    [SerializeField] private Transform firePoint = null;
    [Tooltip("Projectile prefab. Can have either Rigidbody2D or Rigidbody.")]
    [SerializeField] private GameObject projectilePrefab = null;

    [Header("Firing")]
    [Tooltip("Speed applied to the projectile")]
    [SerializeField] private float projectileSpeed = 15f;
    [Tooltip("Minimum time between shots (seconds)")]
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private bool holdToFire = false; // if true, hold Space to fire repeatedly

    [Header("Aiming")]
    [Tooltip("If true, physics and projectiles use 2D (XY). If false, uses 3D (XZ).")]
    [SerializeField] private bool use2D = true;
    [Tooltip("Smooth rotation speed (0 = instant)")]
    [SerializeField] private float aimSmoothTime = 0.02f;
    [Tooltip("Add a rotation offset (degrees) if your sprite/model forward doesn't match +X")]
    [SerializeField] private float rotationOffset = 0f;

    private Vector2 lastAim = Vector2.right;
    private float nextFireTime = 0f;
    private float currentSmoothVelocity = 0f; // used for smooth damping of angle

    void Reset()
    {
        // Try to auto-assign a firePoint if none set
        if (firePoint == null && transform.childCount > 0)
            firePoint = transform.GetChild(0);
    }

    void Update()
    {
        ReadAimInput();
        ApplyRotation();

        bool firePressed = holdToFire ? Input.GetKey(KeyCode.Space) : Input.GetKeyDown(KeyCode.Space);
        if (firePressed && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void ReadAimInput()
    {
        // Uses Unity's default axes (WASD / arrow keys). Using GetAxisRaw gives immediate directions.
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // If there's input (WASD), update lastAim. Keeps last direction when keys are released.
        if (input.sqrMagnitude > 0.001f)
            lastAim = input.normalized;
    }

    private void ApplyRotation()
    {
        // Compute target angle in degrees (0 degrees = +X)
        float targetAngle = Mathf.Atan2(lastAim.y, lastAim.x) * Mathf.Rad2Deg + rotationOffset;

        if (use2D)
        {
            // For 2D, rotate around Z axis
            if (aimSmoothTime <= 0f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
            }
            else
            {
                // SmoothDampAngle for smooth rotation
                float z = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref currentSmoothVelocity, aimSmoothTime);
                transform.rotation = Quaternion.Euler(0f, 0f, z);
            }
        }
        else
        {
            // For 3D top-down (XZ plane), create a forward direction and rotate to face it.
            Vector3 dir = new Vector3(lastAim.x, 0f, lastAim.y);
            if (dir.sqrMagnitude < 0.0001f) return;

            Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(0f, rotationOffset, 0f);
            if (aimSmoothTime <= 0f)
                transform.rotation = targetRot;
            else
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime / Mathf.Max(aimSmoothTime, 0.0001f));
        }
    }

    private void Fire()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Gun: projectilePrefab or firePoint not assigned.");
            return;
        }

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        if (use2D)
        {
            Rigidbody2D rb2d = proj.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                rb2d.linearVelocity = lastAim * projectileSpeed;
            }
            else
            {
                // Fallback: translate manually if no Rigidbody2D
                proj.transform.position += (Vector3)lastAim * 0.01f;
                Debug.LogWarning("Gun: spawned projectile has no Rigidbody2D. Consider adding one for proper physics.");
            }
        }
        else
        {
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            Vector3 dir3 = new Vector3(lastAim.x, 0f, lastAim.y).normalized;
            if (rb != null)
            {
                rb.linearVelocity = dir3 * projectileSpeed;
            }
            else
            {
                proj.transform.position += dir3 * 0.01f;
                Debug.LogWarning("Gun: spawned projectile has no Rigidbody. Consider adding one for proper physics.");
            }
        }
    }
}
