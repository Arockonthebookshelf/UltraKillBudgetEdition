using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas deathCanvas;

    bool gamePaused;

    void Update()
    {
        //redo inputs using new input system
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
        Time.timeScale = 0f;
        pauseCanvas.enabled = true;
        //disable player input
    }

    public void Resume()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        pauseCanvas.enabled = false;
        //enable player input
    }

    public void Dead()
    {
        deathCanvas.enabled = true;
        Time.timeScale = 0f;
    }

    public void Respawn()
    {
        //respawn pkayer
        deathCanvas.enabled = false;
        Time.timeScale = 1f;
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
