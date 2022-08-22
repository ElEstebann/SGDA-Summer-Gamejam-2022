using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuSettingsChanger : MonoBehaviour
{
    GameSettings gameSettings;

    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SoundVolumeSlider;
    

    // Start is called before the first frame update
    void Start()
    {
        gameSettings = FindObjectOfType<GameSettings>();

        Debug.Log(gameSettings.masterVolume + "");
        Debug.Log(gameSettings.musicVolume + "");
        Debug.Log(gameSettings.soundVolume + "");

        float startingMasterVolume = gameSettings.masterVolume;
        float startingMusicVolume = gameSettings.musicVolume;
        float startingSoundVolume = gameSettings.soundVolume;

        MasterVolumeSlider.value = startingMasterVolume;
        MusicVolumeSlider.value = startingMusicVolume;
        SoundVolumeSlider.value = startingSoundVolume;

        Debug.Log(gameSettings.masterVolume + "");
        Debug.Log(gameSettings.musicVolume + "");
        Debug.Log(gameSettings.soundVolume + "");
    }

    public void UpdateGameSettings() {
        gameSettings.masterVolume = MasterVolumeSlider.value;
        gameSettings.musicVolume = MusicVolumeSlider.value;
        gameSettings.soundVolume = SoundVolumeSlider.value;
    }
}
