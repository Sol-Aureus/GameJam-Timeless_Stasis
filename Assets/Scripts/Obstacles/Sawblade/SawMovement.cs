using UnityEngine;

public class SawMovement : MonoBehaviour
{
    [Header("Saw Movement Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float range = 50f;

    [Header("References")]
    [SerializeField] private Transform blades;
    [SerializeField] private MeshRenderer[] sawMeshRenderers;
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material slowMaterial;
    [SerializeField] private Material freezeMaterial;
    [SerializeField] private Material pastMaterial;

    private float currentRotationSpeed;
    private float currentMoveSpeed;
    private float distanceTraveled = 0f;

    private bool isFrozen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetSpeed();
    }

    // Subscribe to the player respawn event
    void OnEnable()
    {
        PlayerDeath.OnPlayerRespawned += LockInPast;
    }

    // Unsubscribe from the player respawn event
    void OnDisable()
    {
        PlayerDeath.OnPlayerRespawned -= LockInPast;
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    private void FixedUpdate()
    {
        if (isFrozen) return;

        // Rotate the saw blades
        blades.Rotate(Vector3.up * currentRotationSpeed * Time.fixedDeltaTime);

        // Move the saw forward
        transform.Translate(Vector3.forward * currentMoveSpeed * Time.fixedDeltaTime);
        distanceTraveled += currentMoveSpeed * Time.fixedDeltaTime;
        if (distanceTraveled >= range)
        {
            Destroy(gameObject);
        }
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
        for (int j = 0; j < sawMeshRenderers.Length; j++)
        {
            sawMeshRenderers[j].material = slowMaterial;
        }
    }

    // Apply freezing effect to the saw
    public void SetFreeze()
    {
        isFrozen = true;
        for (int j = 0; j < sawMeshRenderers.Length; j++)
        {
            sawMeshRenderers[j].material = freezeMaterial;
        }
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
        for (int j = 0; j < sawMeshRenderers.Length; j++)
        {
            sawMeshRenderers[j].material = baseMaterial;
        }
    }

    // Lock the saw's appearance to indicate it is in the past state
    private void LockInPast()
    {
        if (!isFrozen)
        {
            Destroy(this.gameObject);
            return;
        }
        for (int j = 0; j < sawMeshRenderers.Length; j++)
        {
            sawMeshRenderers[j].material = pastMaterial;
        }
    }
}
