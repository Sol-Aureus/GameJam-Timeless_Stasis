using Unity.VisualScripting;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public static DoNotDestroy instance;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // Prevents the music manager from being destroyed when loading a new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
