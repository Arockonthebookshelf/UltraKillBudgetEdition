using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Refrences")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject creditsMenu;


    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        creditsMenu.SetActive(false);
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
