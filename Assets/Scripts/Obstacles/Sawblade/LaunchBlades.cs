using UnityEngine;

public class LaunchBlades : MonoBehaviour
{
    [Header("Blade Launch Settings")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float launchSpeed;
    [SerializeField] private float launchInterval;
    [SerializeField] private float range;

    [Header("References")]
    [SerializeField] private GameObject bladePrefab;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Transform target;

    private float launchTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        launchTimer += Time.deltaTime;

        if (IsTargetInRange())
        {
            AimAtTarget();
            if (launchTimer >= launchInterval)
            {
                launchTimer = 0f;
                LaunchBlade();
            }
        }
    }

    // Launch a saw blade at set intervals
    private void LaunchBlade()
    {
        GameObject blade = Instantiate(bladePrefab, launchPoint.position, launchPoint.rotation);
        blade.GetComponent<SawMovement>().SetMoveSpeed(launchSpeed, rotationSpeed);
    }

    // Aim the launcher at the target
    private void AimAtTarget()
    {
        transform.LookAt(target);
    }

    // Check if the target is within range
    private bool IsTargetInRange()
    {
        return Vector3.Distance(transform.position, target.position) <= range;
    }

    // Visualize the launch range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
