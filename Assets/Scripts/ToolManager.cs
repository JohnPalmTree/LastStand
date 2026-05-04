using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public GameObject gun;
    public GameObject knife;

    private bool gunEquipped;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        equipGun();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) equipGun();
        if (Input.GetKeyDown(KeyCode.Alpha2)) equipKnife();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) equipGun();
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) equipKnife();
    }

    void equipGun() {
        gun.SetActive(true); // i think this is how you enable/disable gameobj's but look @ the documentation after work
        knife.SetActive(false);
        gunEquipped = true;
        //Debug.Log("Equipped M1911.");
    }

    void equipKnife() {
        gun.SetActive(false); // i think this is how you enable/disable gameobj's but look @ the documentation after work
        knife.SetActive(true);
        gunEquipped = false;
        //Debug.Log("Equipped Knife.");
    }
}
