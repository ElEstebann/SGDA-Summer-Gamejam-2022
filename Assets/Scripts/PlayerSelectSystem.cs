using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectSystem : MonoBehaviour {
    

    private MultiplayerManager multiplayerManager;

    public PlayerCharacterAndOptions[] playerCharacterAndOptions = new PlayerCharacterAndOptions[4];

    public MenuUIFlavorTextPanels[] flavorTextPanels = new MenuUIFlavorTextPanels[4];


    private void SelectControlType(int player, PlayerCharacterAndOptions.ControlSelected controlSelected, int controlNum) {
        MultiplayerManager.controlType controlType = MultiplayerManager.controlType.Keyboard1;

        if (controlSelected == PlayerCharacterAndOptions.ControlSelected.Gamepad) {
            if (controlNum == 1) {
                controlType = MultiplayerManager.controlType.Gamepad1;
            } else if (controlNum == 2) {
                controlType = MultiplayerManager.controlType.Gamepad2;
            } else if (controlNum == 3) {
                controlType = MultiplayerManager.controlType.Gamepad3;
            } else if (controlNum == 4) {
                controlType = MultiplayerManager.controlType.Gamepad4;
            } else {
                Debug.LogError("Control number does not exist / is not implemented");
            }
        } else if (controlSelected == PlayerCharacterAndOptions.ControlSelected.Keyboard) {
            if (controlNum == 1) {
                controlType = MultiplayerManager.controlType.Keyboard1;
            } else if (controlNum == 2) {
                controlType = MultiplayerManager.controlType.Keyboard2;
            } else if (controlNum == 3) {
                controlType = MultiplayerManager.controlType.Keyboard3;
            } else if (controlNum == 4) {
                controlType = MultiplayerManager.controlType.Keyboard4;
            } else {
                Debug.LogError("Control number does not exist / is not implemented");
            }
        } else if (controlSelected == PlayerCharacterAndOptions.ControlSelected.CPU) {
            controlType = MultiplayerManager.controlType.CPU;
        } else {
            Debug.LogError("Control type does not exist / is not implemented");
        }

        multiplayerManager.playerControls[player] = controlType;
    }
    private void SelectSprite(int player, Sprite sprite) {
        multiplayerManager.playerSprites[player] = sprite;
        flavorTextPanels[player].SelectPanel(playerCharacterAndOptions[player].playerCurrentSpriteDisplayed);
    }

    public void ChangePlayerCharacterAndOptions(int player, PlayerCharacterAndOptions.ControlSelected controlSelected, int controlNum) {
        playerCharacterAndOptions[player].SelectControl(controlSelected);
        playerCharacterAndOptions[player].SelectControlNum(controlNum);
    }

    private void Start() {
        multiplayerManager = MultiplayerManager.instance;

        // Set the options currently selected to whatever is in the multiplayer manager!
        for (int i = 0; i < 4; i++) {
            SelectSprite(i, playerCharacterAndOptions[i].playerSprites[0]);

            if (multiplayerManager.playerControls[i] == MultiplayerManager.controlType.Keyboard1) {
                ChangePlayerCharacterAndOptions(i, PlayerCharacterAndOptions.ControlSelected.Keyboard, 1);
            } else if (multiplayerManager.playerControls[i] == MultiplayerManager.controlType.Keyboard2) {
                ChangePlayerCharacterAndOptions(i, PlayerCharacterAndOptions.ControlSelected.Keyboard, 2);
            } else if (multiplayerManager.playerControls[i] == MultiplayerManager.controlType.Keyboard3) {
                ChangePlayerCharacterAndOptions(i, PlayerCharacterAndOptions.ControlSelected.Keyboard, 3);
            } else if (multiplayerManager.playerControls[i] == MultiplayerManager.controlType.Keyboard4) {
                ChangePlayerCharacterAndOptions(i, PlayerCharacterAndOptions.ControlSelected.Keyboard, 4);
            } else if (multiplayerManager.playerControls[i] == MultiplayerManager.controlType.Gamepad1) {
                ChangePlayerCharacterAndOptions(i, PlayerCharacterAndOptions.ControlSelected.Gamepad, 1);
            } else if (multiplayerManager.playerControls[i] == MultiplayerManager.controlType.Gamepad2) {
                ChangePlayerCharacterAndOptions(i, PlayerCharacterAndOptions.ControlSelected.Gamepad, 2);
            } else if (multiplayerManager.playerControls[i] == MultiplayerManager.controlType.Gamepad3) {
                ChangePlayerCharacterAndOptions(i, PlayerCharacterAndOptions.ControlSelected.Gamepad, 3);
            } else if (multiplayerManager.playerControls[i] == MultiplayerManager.controlType.Gamepad4) {
                ChangePlayerCharacterAndOptions(i, PlayerCharacterAndOptions.ControlSelected.Gamepad, 4);
            } else if (multiplayerManager.playerControls[i] == MultiplayerManager.controlType.CPU) {
                ChangePlayerCharacterAndOptions(i, PlayerCharacterAndOptions.ControlSelected.Gamepad, 1);
            } else {
                Debug.LogError("Control type does not exist / is not implemented");
            }
        }
    }

    private void Update() {
        for (int i = 0; i < 4; i++) {
            SelectControlType(i, playerCharacterAndOptions[i].controlSelected, playerCharacterAndOptions[i].controlNumSelected);
            SelectSprite(i, playerCharacterAndOptions[i].playerSprites[playerCharacterAndOptions[i].playerCurrentSpriteDisplayed]);
            MultiplayerManager.instance.characters[i] = (MultiplayerManager.characterType)playerCharacterAndOptions[i].playerCurrentSpriteDisplayed;
        }
    }
}
