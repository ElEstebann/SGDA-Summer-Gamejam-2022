using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterAndOptions : MonoBehaviour
{

    public Sprite[] playerSprites = new Sprite[4];
    public Image playerCharacterDisplay;
    public int playerCurrentSpriteDisplayed;
    public ControlSelected controlSelected;

    public GameObject gamepadSelectFrame;
    public GameObject keyboardSelectFrame;
    public GameObject CPUSelectFrame;

    public enum ControlSelected {
        CPU,
        Keyboard,
        Gamepad
    }

    public void ScrollCharacterDisplayLeft() {
        playerCurrentSpriteDisplayed -= 1;
        if (playerCurrentSpriteDisplayed < 0) {
            playerCurrentSpriteDisplayed = 3;
        }
        playerCharacterDisplay.sprite = playerSprites[playerCurrentSpriteDisplayed];
    }

    public void ScrollCharacterDisplayRight() {
        playerCurrentSpriteDisplayed += 1;
        if (playerCurrentSpriteDisplayed > 3) {
            playerCurrentSpriteDisplayed = 0;
        }
        playerCharacterDisplay.sprite = playerSprites[playerCurrentSpriteDisplayed];
    }

    public void SelectGamepad() {
        controlSelected = ControlSelected.Gamepad;
        gamepadSelectFrame.SetActive(true);
        keyboardSelectFrame.SetActive(false);
        CPUSelectFrame.SetActive(false);
    }
    public void SelectKeyboard() {
        controlSelected = ControlSelected.Keyboard;
        gamepadSelectFrame.SetActive(false);
        keyboardSelectFrame.SetActive(true);
        CPUSelectFrame.SetActive(false);
    }
    public void SelectCPU() {
        controlSelected = ControlSelected.CPU;
        gamepadSelectFrame.SetActive(false);
        keyboardSelectFrame.SetActive(false);
        CPUSelectFrame.SetActive(true);
    }
}
