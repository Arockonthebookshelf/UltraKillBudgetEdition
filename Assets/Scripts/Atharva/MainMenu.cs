using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Canvas Refrences")]
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas levelSelectCanvas;
    [SerializeField] Canvas settingsCanvas;
    [SerializeField] Canvas creditsCanvas;


    public void LevelSelect()
    {
        mainCanvas.enabled = false;
        levelSelectCanvas.enabled = true;
    }

    public void Settings()
    {
        mainCanvas.enabled = false;
        settingsCanvas.enabled = true;
    }

    public void Credits()
    {
        mainCanvas.enabled = false;
        creditsCanvas.enabled = true;
    }

    public void ReturnToMainMenu()
    {
        mainCanvas.enabled = true;
        settingsCanvas.enabled = false;
        levelSelectCanvas.enabled = false;
        creditsCanvas.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel1()
    {
        
    }

    public void LoadLevel2()
    {
        
    }

    public void LoadLevel3()
    {
        
    }
}
