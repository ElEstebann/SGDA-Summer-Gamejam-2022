using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCharacterAndOptions : MonoBehaviour
{

    public Sprite[] playerSprites = new Sprite[4];
    public Image playerCharacterDisplay;
    public int playerCurrentSpriteDisplayed;

    public TextMeshProUGUI controlNumText;

    public ControlSelected controlSelected;
    public int controlNumSelected = 1;

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

    public void ScrollControlNumUp() {
        controlNumSelected++;
        if (controlNumSelected > 4) {
            controlNumSelected = 1;
        }
        controlNumText.text = controlNumSelected + "";
    }
    public void ScrollControlNumDown() {
        controlNumSelected--;
        if (controlNumSelected < 1) {
            controlNumSelected = 4;
        }
        controlNumText.text = controlNumSelected + "";
    }

    public void SelectControl(ControlSelected control) {
        if (control == ControlSelected.Gamepad) {
            SelectGamepad();
        } else if (control == ControlSelected.Keyboard) {
            SelectKeyboard();
        } else if (control == ControlSelected.CPU) {
            SelectCPU();
        } else {
            Debug.LogError("Control selection unimplemented");
        }
    }
    public void SelectControlNum(int controlNum) {
        controlNumSelected = controlNum;
        controlNumText.text = controlNumSelected + "";
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
