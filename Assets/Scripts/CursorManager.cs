using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private bool lockCursorOnStart = true;

    void Start()
    {
        if (lockCursorOnStart)
        {
            LockCursor();
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Escape to free the cursor (testing purposes)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }

        if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.None)
        {
            LockCursor();
        }
    }
}