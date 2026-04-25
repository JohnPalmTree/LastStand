using UnityEngine;

public class RayTest : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)
        );
        
        Debug.Log("Camera forward: " + Camera.main.transform.forward);
        Debug.Log("Ray direction: " + ray.direction);
        Debug.Log("Are they the same? " + (ray.direction == Camera.main.transform.forward));
        
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100f, Color.blue);
    }
}