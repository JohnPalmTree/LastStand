using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 8f;
    public float verticalSens = 100f;

    private float pitch = 10f; // starting vertical angle
    private float distance = 4f;
    private float height = 1.5f;

    void LateUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y") * verticalSens * Time.deltaTime;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -20f, 60f);

        // Position camera behind player based on their Y rotation + our pitch
        Quaternion rotation = Quaternion.Euler(pitch, player.eulerAngles.y, 0f);
        Vector3 desiredPosition = player.position + Vector3.up * height + rotation * Vector3.back * distance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(player.position + Vector3.up * height);
    }
}