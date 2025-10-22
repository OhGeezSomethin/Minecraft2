using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform targetChara;
    public InputAction mouseMove;

    [SerializeField] private Vector3 camOffset;

    [SerializeField] private float sensitivity = 50f;
    [SerializeField] private float minY = -90f;
    [SerializeField] private float maxY = 90f;
    private float rotationY;
    private float rotationX;

    // LateUpdate is called after all updates
    void LateUpdate()
    {
        Vector2 look = mouseMove.ReadValue<Vector2>();

        rotationX -= look.y * sensitivity * Time.deltaTime;
        rotationY += look.x * sensitivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, minY, maxY);

        Quaternion camRotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 camPos = targetChara.position + camRotation * camOffset;

        transform.position = camPos;
        transform.LookAt(targetChara.position + Vector3.up);
    }

    public void SetTarget(Transform newTarget)
    {
        targetChara = newTarget;
    }

    public void SetRotation(float newY)
    {
        rotationY = newY;
    }

    void OnEnable()
    {
        mouseMove.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        mouseMove.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
