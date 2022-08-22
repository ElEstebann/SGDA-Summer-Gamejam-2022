using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;

    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float soundVolume = 1f;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    public float GetMusicVolume() {
        return masterVolume * musicVolume;
    }
    public float GetSoundVolume() {
        return masterVolume * soundVolume;
    }
}
