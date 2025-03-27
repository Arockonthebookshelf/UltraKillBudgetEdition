using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;

    bool gamePaused = false;

    private void Awake()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gamePaused)
            {
                PauseGame();
            }
            else
            {
                Resume();
            }
        }
    }

    void PauseGame()
    {
        playerMovement.inputEnabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gamePaused = true;
        Time.timeScale =0f;
        pauseMenu.SetActive(true);
        //disable player input
    }

    public void Resume()
    {
        playerMovement.inputEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gamePaused = false;
        Time.timeScale =1f;
        pauseMenu.SetActive(false);
        //enable player input
    }

    public void Dead()
    { 

        playerMovement.inputEnabled = false;
        if(deathMenu != null) deathMenu.SetActive(true);
    }

    public void Respawn()
    {
        playerMovement.inputEnabled = true;
        if (deathMenu != null) deathMenu.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        playerMovement.inputEnabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
