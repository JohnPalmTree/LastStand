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

        //Debug.Log("loading knife...");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && Time.time >= nextFireTime) {
            //Debug.Log("fire?");
                
            Fire();
        }
    }

    void Fire()
    {
        nextFireTime = Time.time + fireRate;

        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)
        );

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green, 1f);
            //Debug.Log("Hit: " + hit.collider.name);

            ZombieHealth zombie = hit.collider.GetComponent<ZombieHealth>();
            if (zombie != null) zombie.TakeDamage(dmg);
        }
    }
}