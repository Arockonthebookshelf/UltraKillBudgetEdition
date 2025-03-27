using UnityEngine;

[CreateAssetMenu(fileName = "SettingValue", menuName = "Scriptable Objects/SettingValue")]
public class SettingValue : ScriptableObject
{
    public int masterVolume;
    public int MusicVolume;
    public int SFXVolume;
    public int MouseSensitivity;
    public bool vignette;
    public bool motionBlur;
}
