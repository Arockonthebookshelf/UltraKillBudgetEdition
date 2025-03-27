using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettingsManager : MonoBehaviour
{
    [SerializeField] SettingValue settings;

    [Header("AUDIO SETTINGS")]
    public GameObject masterVolumeSlider;
    public GameObject musicSlider;
    public GameObject sfxSlider;

    [Header("VIDEO SETTINGS")]
    public GameObject fullscreentext;
    public GameObject vsynctext;
    public GameObject motionblurtext;

    [Header("CONTROLS SETTINGS")]
    public GameObject sensitivitySlider;

    public void Start()
    {
        // check slider values
        masterVolumeSlider.GetComponent<Slider>().value = settings.masterVolume;
        musicSlider.GetComponent<Slider>().value = settings.MusicVolume;
        sfxSlider.GetComponent<Slider>().value = settings.SFXVolume;
        sensitivitySlider.GetComponent<Slider>().value = settings.MouseSensitivity;

        // check full screen
        if (Screen.fullScreen == true)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "ON";
        }
        else if (Screen.fullScreen == false)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "OFF";
        }

        // check vsync
        if (QualitySettings.vSyncCount == 0)
        {
            vsynctext.GetComponent<TMP_Text>().text = "OFF";
        }
        else if (QualitySettings.vSyncCount == 1)
        {
            vsynctext.GetComponent<TMP_Text>().text = "ON";
        }
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen == true)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "ON";
        }
        else if (Screen.fullScreen == false)
        {
            fullscreentext.GetComponent<TMP_Text>().text = "OFF";
        }
    }

    public void MasterVolumeSlider()
    {
        settings.masterVolume = masterVolumeSlider.GetComponent<Slider>().value;
    }
    public void MusicSlider()
    {
        settings.MusicVolume = musicSlider.GetComponent<Slider>().value;
    }
    public void SFXSlider()
    {
        settings.SFXVolume = sfxSlider.GetComponent<Slider>().value;
    }

    public void SensitivitySlider()
    {
        settings.MouseSensitivity = sensitivitySlider.GetComponent<Slider>().value;
    }

    public void vsync()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            QualitySettings.vSyncCount = 1;
            vsynctext.GetComponent<TMP_Text>().text = "ON";
        }
        else if (QualitySettings.vSyncCount == 1)
        {
            QualitySettings.vSyncCount = 0;
            vsynctext.GetComponent<TMP_Text>().text = "OFF";
        }
    }
}