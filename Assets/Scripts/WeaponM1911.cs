using UnityEngine;

public class WeaponM1911 : MonoBehaviour
{
    [Header("Stats")]
    public int damage = 50; // typically 15
    public int magSize = 7;
    public int maxMags = 22; // typically 7
    public float fireRate = 0.5f;
    public float reloadTime = 1.5f;

    [Header("State")]
    public int currentAmmo;
    public int currentMags;

    private float nextFireTime = 0f;
    private bool isReloading = false;

    [Header("References")]
    public Transform muzzle; // Empty GameObject at the tip of the gun
    public CameraFollow cameraFollow;

    void Start()
    {
        currentAmmo = magSize;
        currentMags = maxMags;
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetButtonDown("Fire") && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0) Fire();
            else StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magSize && currentMags > 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Fire()
    {
        currentAmmo--;
        nextFireTime = Time.time + fireRate;

        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)
        );

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green, 1f);
            //Debug.Log("Hit: " + hit.collider.name);

            ZombieHealth zombie = hit.collider.GetComponentInParent<ZombieHealth>();
            if (zombie != null) zombie.TakeDamage(damage);
        }
    }

    System.Collections.IEnumerator Reload()
    {
        if (currentMags <= 0) yield break;

        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentMags--;
        currentAmmo = magSize;
        isReloading = false;
    }
}