using UnityEngine;

public class SawMovement : MonoBehaviour
{
    [Header("Saw Movement Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float moveSpeed = 50f;

    [Header("References")]
    [SerializeField] private Transform blades;

    private float currentRotationSpeed;
    private float currentMoveSpeed;

    private bool isFrozen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetSpeed();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    private void FixedUpdate()
    {
        if (isFrozen) return;

        // Rotate the saw blades
        blades.Rotate(Vector3.up * currentRotationSpeed * Time.fixedDeltaTime);

        // Move the saw forward
        transform.Translate(Vector3.forward * currentMoveSpeed * Time.fixedDeltaTime);
    }

    // Set the movement speed of the saw
    public void SetMoveSpeed(float newSpeed, float newRotationSpeed)
    {
        rotationSpeed = newRotationSpeed;
        moveSpeed = newSpeed;
    }

    // Apply slowing effect to the saw
    public void SetSlow()
    {
        currentRotationSpeed = rotationSpeed * 0.5f;
        currentMoveSpeed = moveSpeed * 0.5f;
    }

    // Apply freezing effect to the saw
    public void SetFreeze()
    {
        currentRotationSpeed = 0f;
        currentMoveSpeed = 0f;
        isFrozen = true;
    }

    // Check if the saw is frozen
    public bool IsFreeze()
    {
        return isFrozen;
    }

    // Reset the saw's speed to its original values
    public void ResetSpeed()
    {
        currentRotationSpeed = rotationSpeed;
        currentMoveSpeed = moveSpeed;
    }
}
