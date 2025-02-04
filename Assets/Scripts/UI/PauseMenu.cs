using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;

    bool gamePaused;

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
        gamePaused = true;
        pauseMenu.SetActive(true);
        //disable player input
    }

    public void Resume()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
        //enable player input
    }

    public void Dead()
    {
        if(deathMenu != null) deathMenu.SetActive(true);
    }

    public void Respawn()
    {
        //respawn pkayer
        if(deathMenu != null) deathMenu.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
