using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuFunctions: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;

    private InputAction pauseAction;

    private bool isPaused = false;
    private bool isWin = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Cancel");

        pauseAction.performed += TriggerPause;

        winScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Trigger the win condition
    public void TriggerWin()
    {
        winScreen.SetActive(true);
        isWin = true;
        Time.timeScale = 0f; // Pause the game

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Trigger the pause menu
    public void TriggerPause(InputAction.CallbackContext context)
    {
        if (isWin) return;
        Pause();
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(isPaused);
            Time.timeScale = 0f; // Pause the game

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            isPaused = false;
            pauseScreen.SetActive(isPaused);
            Time.timeScale = 1f; // Pause the game

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Restart the current game scene
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Return to the main menu scene
    public void MainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
