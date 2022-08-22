using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUISoundPlayer : MonoBehaviour
{
    MenuUISoundPlayerSingleton menuUISoundPlayerSingleton;

    // Start is called before the first frame update
    void Start() {
        menuUISoundPlayerSingleton = MenuUISoundPlayerSingleton.instance;
    }

    public void PlayBackoutNoise() {
        menuUISoundPlayerSingleton.PlayBackoutNoise();
    }
    public void PlayMoveAroundNoise() {
        menuUISoundPlayerSingleton.PlayMoveAroundNoise();
    }
    public void PlaySelectNoise() {
        menuUISoundPlayerSingleton.PlaySelectNoise();
    }
}
