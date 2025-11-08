using Unity.VisualScripting;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [Header("Kill Player Settings")]
    [SerializeField] private float gracePeriod = 0.12f;

    private float killTimer = 0f;

    // Called when another collider stays within the trigger collider
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            killTimer += Time.deltaTime;
            if (killTimer >= gracePeriod)
            {
                other.gameObject.GetComponent<PlayerDeath>().RespawnPlayer();
            }
        }
    }

    // Called when another collider exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            killTimer = 0f;
        }
    }
}
