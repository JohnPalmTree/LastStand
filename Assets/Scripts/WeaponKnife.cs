using UnityEngine;

public class WeaponKnife : MonoBehaviour
{
    [Header("Weapon Stats")]
    public int dmg = 50;
    public float fireRate = 0.7f;
    public float Range = 2f;

    [Header("Weapon States")]
    private float nextFireTime;
    private bool canFire;

    [Header("References")]
    public Camera playerCamera;

    void Start()
    {
        nextFireTime = 0f;
        canFire = true;

        Debug.Log("loading knife...");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && Time.time >= nextFireTime) {
            Debug.Log("fire?");
                
            Fire();
        }
    }

    void Fire() {
        if (canFire == false) return;

        nextFireTime = Time.time + fireRate;

        // hit-scan raycast
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * Range, Color.yellow, 0.5f);

        if (Physics.Raycast(ray, out hit, Range)) {
            Debug.DrawRay(ray.origin, ray.direction * Range, Color.green, 1f);
            Debug.Log("KnifeHit: " + hit.collider.name);

            // zombie stuff..
        }

        canFire = true;        
    }
}
