using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Rigidbody rb;

    public delegate void PlayerRespawned();
    public static event PlayerRespawned OnPlayerRespawned;

    // Respawn the player at the respawn point
    public void RespawnPlayer()
    {
        // Reset position
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        // Reset velocity
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Notify subscribers that the player has respawned
        OnPlayerRespawned?.Invoke();
    }
}
