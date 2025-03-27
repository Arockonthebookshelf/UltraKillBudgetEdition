using UnityEngine;

[CreateAssetMenu(fileName = "SettingValue", menuName = "Scriptable Objects/SettingValue")]
public class SettingValue : ScriptableObject
{
    public float masterVolume;
    public float MusicVolume;
    public float SFXVolume;
    public float MouseSensitivity;
}
