using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform targetTransform;

    // LateUpdate is called once per frame, after all Update functions have been called
    void LateUpdate()
    {
        transform.position = targetTransform.position;
    }
}
