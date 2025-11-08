using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private InputAction lookAction;

    [Header("Camera Settings")]
    [SerializeField] private float sensX = 40f;
    [SerializeField] private float sensY = 40f;

    [Header("References")]
    [SerializeField] private Transform orientation;

    private float rotationY;
    private float rotationX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lookAction = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookVector = lookAction.ReadValue<Vector2>();

        rotationY += lookVector.x * sensY * Time.deltaTime;
        rotationX -= lookVector.y * sensX * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
    }

    // LateUpdate is called once per frame, after all Update functions have been called
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
