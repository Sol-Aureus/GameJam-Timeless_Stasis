using UnityEngine;

public class WallMovement : MonoBehaviour
{

    [Header("Wall Movement Settings")]
    [SerializeField] private int startingPoint = 0;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float acceptableDist = 1f;
    [SerializeField] private float rotationSpeed = 300f;

    [Header("References")]
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform wallTransform;

    private float sinTime;
    private int i;

    private float currentMoveSpeed;
    private float currentRotationSpeed;
    private bool isFrozen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetWallPosition();
        PlayerDeath.OnPlayerRespawned += ResetWallPosition;
        ResetSpeed();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        if (isFrozen) return;

        if (transform.position != points[i].position)
        {
            sinTime += (currentMoveSpeed * Time.deltaTime) / 100;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = evaluate(sinTime);
            transform.position = Vector3.Lerp(transform.position, points[i].position, t);
        }

        if (Vector3.Distance(transform.position, points[i].position) < acceptableDist / 100)
        {
            i++;
            sinTime = 0;
            if (i >= points.Length)
            {
                i = 0;
            }
        }

        transform.LookAt(points[i].position);

        RotateWall();
    }

    // Sine wave function to smooth movement
    private float evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5f;
    }

    private void RotateWall()
    {
        wallTransform.Rotate(Vector3.right * currentRotationSpeed * Time.fixedDeltaTime);
    }

    // Reset wall position to starting point
    public void ResetWallPosition()
    {
        transform.position = points[startingPoint].position;
        i = startingPoint;
        sinTime = 0;
    }

    // Apply slowing effect to the saw
    public void SetSlow()
    {
        currentMoveSpeed = moveSpeed * 0.5f;
        currentRotationSpeed = rotationSpeed * 0.5f;
    }

    // Apply freezing effect to the saw
    public void SetFreeze()
    {
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
        currentMoveSpeed = moveSpeed;
        currentRotationSpeed = rotationSpeed;
    }
}
