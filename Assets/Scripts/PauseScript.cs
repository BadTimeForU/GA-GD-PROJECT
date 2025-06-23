using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Alles van je Pause menu")]
    public GameObject pauseMenuUI; // Hele Pause menu: logo, knoppen, achtergrond

    [Header("Je HUD en Gun")]
    public GameObject playerHUD;   // Canvas of UI van je ammo/health bar
    public GameObject playerGun;   // Het Gun GameObject

    private bool isPaused = false;

    void Start()
    {
        // Bij start: muis locken en onzichtbaar
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Zorg dat Pause menu UIT is bij start
        pauseMenuUI.SetActive(false);

        // Zorg dat HUD en Gun AAN zijn bij start
        if (playerHUD != null) playerHUD.SetActive(true);
        if (playerGun != null) playerGun.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        // Zet HUD en Gun weer AAN
        if (playerHUD != null) playerHUD.SetActive(true);
        if (playerGun != null) playerGun.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);

        // Zet HUD en Gun UIT
        if (playerHUD != null) playerHUD.SetActive(false);
        if (playerGun != null) playerGun.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
