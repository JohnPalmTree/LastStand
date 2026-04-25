using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float distance = 4f;
    public float height = 2f;
    public float verticalSens = 2f;
    public float pitchMin = -20f;
    public float pitchMax = 40f;

    private float pitch = 0f;

    void Start()
    {
        // Snap to correct position immediately
        UpdatePosition();
    }

    void LateUpdate()
    {
        pitch -= Input.GetAxis("Mouse Y") * verticalSens;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        UpdatePosition();
    }

    void UpdatePosition()
    {
        Quaternion yaw = Quaternion.Euler(0f, player.eulerAngles.y, 0f);
        Quaternion fullRotation = Quaternion.Euler(pitch, player.eulerAngles.y, 0f);

        // Position camera behind and above player
        transform.position = player.position
            + Vector3.up * height
            + fullRotation * Vector3.back * distance;

        // Always look at player's head level
        transform.LookAt(player.position + Vector3.up * (height * 0.5f));
    }

    // Returns the world point the crosshair is aimed at
    public Vector3 GetAimPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
        {
            return hit.point;
        }

        return ray.origin + ray.direction * 500f;
    }
}