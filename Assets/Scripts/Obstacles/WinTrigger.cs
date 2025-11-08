using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MenuFunctions menuFunctions;

    // Called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            menuFunctions.TriggerWin();
        }
    }
}
