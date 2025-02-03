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
        pauseCanvas.enabled = true;
        //disable player input
    }

    public void Resume()
    {
        gamePaused = false;
        pauseCanvas.enabled = false;
        //enable player input
    }

    public void Dead()
    {
        //deathCanvas.enabled = true;
    }

    public void Respawn()
    {
        //respawn pkayer
        //deathCanvas.enabled = false;
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
