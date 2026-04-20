using UnityEngine;

public class WeaponM1911 : MonoBehaviour
{
    [Header("Weapon Stats")]
    public int dmg = 15;
    public int magSize = 7;
    public int maxMags = 7;
    public float fireRate = 0.5f;
    public float reloadTime = 1.5f;
    public float Range = 100f;

    [Header("Weapon States")]
    public int currentAmmo;
    public int currentMags;

    private float nextFireTime;

    private bool canFire;
    private bool canReload;
    private bool Firing;
    private bool Reloading;

    [Header("References")]
    public Camera playerCamera;

    void Start()
    {
        currentAmmo = magSize;
        currentMags = maxMags;

        nextFireTime = 0f;

        canFire = true;
        canReload = false;
        Firing = false;
        Reloading = false;

        Debug.Log("loading...");
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("gun system loaded...");

        if (Reloading) return;

        if (Input.GetButtonDown("Fire") && Time.time >= nextFireTime) {
            if (currentAmmo > 0) {
                Debug.Log("fire?");
                
                Fire();
            } else {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magSize && currentMags > 0) {
            StartCoroutine(Reload());
        }
    }

    void Fire() {
       if (canFire == false) return;

        currentAmmo--;
        nextFireTime = Time.time + fireRate;

        canFire = false;
        Firing = true;

        Debug.Log("fire!!");


        // hit-scan raycast
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Debug.DrawRay(ray.origin, ray.direction * Range, Color.red, 1f);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Range)) {
            Debug.DrawRay(ray.origin, ray.direction * Range, Color.green, 1f);
            Debug.Log("Hit: " + hit.collider.name);

            // zombie stuff..
        }

        Firing = false;
        canFire = true;
        
        if (canReload == false) canReload = true;
    }

    System.Collections.IEnumerator Reload() {
        if (currentMags <= 0) yield break;

        Reloading = true;
        canFire = false;
        canReload = false;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentMags--;
        currentAmmo = magSize;
        Reloading = false;
        canFire = true;
    }
}