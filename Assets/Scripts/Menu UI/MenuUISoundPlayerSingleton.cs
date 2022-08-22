using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUISoundPlayerSingleton : MonoBehaviour
{
    public static MenuUISoundPlayerSingleton instance;

    GameSettings gameSettings;

    public AudioSource audioSource;

    public AudioClip[] audioClips = new AudioClip[3];

    void Awake() {
        DontDestroyOnLoad(gameObject);
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start() {
        gameSettings = GameSettings.instance;
    }

    private void PlaySound() {
        audioSource.volume = gameSettings.GetSoundVolume();
        audioSource.Play();
    }
    public void PlayBackoutNoise() {
        audioSource.clip = audioClips[0];
        PlaySound();
    }
    public void PlayMoveAroundNoise() {
        audioSource.clip = audioClips[1];
        PlaySound();
    }
    public void PlaySelectNoise() {
        audioSource.clip = audioClips[2];
        PlaySound();
    }


}
