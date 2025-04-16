using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance = null;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject gameOverMenu;

    public MusicManager musicManager;
    public static Action OnRestart;

    bool gamePaused = false;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        PlayerMovement.Instance.inputEnabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gamePaused = true;
        Time.timeScale =0f;
        pauseMenu.SetActive(true);

        musicManager.PauseMusic();
    }

    public void Resume()
    {
        PlayerMovement.Instance.inputEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gamePaused = false;
        Time.timeScale =1f;
        pauseMenu.SetActive(false);
    }

    public void Dead()
    { 
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        PlayerMovement.Instance.inputEnabled = false;
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
        OnRestart?.Invoke();
    }

    public void Respawn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerMovement.Instance.inputEnabled = true;
        if (deathMenu != null) deathMenu.SetActive(false);
    }

    public void Restart()
    {
        OnRestart?.Invoke();
        PlayerMovement.Instance.inputEnabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Player.OnPlayerReloaded?.Invoke();
    }

    public void GameOver()
    {
        PlayerMovement.Instance.inputEnabled = true;
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }

    public void QuitToMainMenu()
    {

        PlayerMovement.Instance.inputEnabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitToDesktop()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
