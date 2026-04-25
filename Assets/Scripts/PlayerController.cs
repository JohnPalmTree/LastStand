using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float mouseSens = 100f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private Transform cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Camera looks up and down
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Player body turns left and right
        transform.Rotate(Vector3.up * mouseX);

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}