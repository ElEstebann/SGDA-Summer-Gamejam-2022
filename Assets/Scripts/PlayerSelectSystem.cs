using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectSystem : MonoBehaviour {
    private enum ControlSelected {
        CPU,
        Keyboard,
        Gamepad
    }

    private MultiplayerManager multiplayerManager;

    public Sprite[] player1Sprites = new Sprite[4];
    public Sprite[] player2Sprites = new Sprite[4];
    public Sprite[] player3Sprites = new Sprite[4];
    public Sprite[] player4Sprites = new Sprite[4];

    private void SelectControlType(int player, ControlSelected control) {
        if (player == 0) {
            if (control == ControlSelected.CPU) {
                multiplayerManager.playerControls[0] = MultiplayerManager.controlType.CPU;
            } else if (control == ControlSelected.Keyboard) {
                multiplayerManager.playerControls[0] = MultiplayerManager.controlType.Keyboard1;
            } else if (control == ControlSelected.Gamepad) {
                multiplayerManager.playerControls[0] = MultiplayerManager.controlType.Gamepad1;
            } else {
                Debug.LogError("Control type does not exist / is not implemented");
            }
        } else if (player == 1) {
            if (control == ControlSelected.CPU) {
                multiplayerManager.playerControls[1] = MultiplayerManager.controlType.CPU;
            } else if (control == ControlSelected.Keyboard) {
                multiplayerManager.playerControls[1] = MultiplayerManager.controlType.Keyboard2;
            } else if (control == ControlSelected.Gamepad) {
                multiplayerManager.playerControls[1] = MultiplayerManager.controlType.Gamepad2;
            } else {
                Debug.LogError("Control type does not exist / is not implemented");
            }
        } else if (player == 2) {
            if (control == ControlSelected.CPU) {
                multiplayerManager.playerControls[2] = MultiplayerManager.controlType.CPU;
            } else if (control == ControlSelected.Keyboard) {
                multiplayerManager.playerControls[2] = MultiplayerManager.controlType.Keyboard3;
            } else if (control == ControlSelected.Gamepad) {
                multiplayerManager.playerControls[2] = MultiplayerManager.controlType.Gamepad3;
            } else {
                Debug.LogError("Control type does not exist / is not implemented");
            }
        } else if (player == 3) {
            if (control == ControlSelected.CPU) {
                multiplayerManager.playerControls[3] = MultiplayerManager.controlType.CPU;
            } else if (control == ControlSelected.Keyboard) {
                multiplayerManager.playerControls[3] = MultiplayerManager.controlType.Keyboard4;
            } else if (control == ControlSelected.Gamepad) {
                multiplayerManager.playerControls[3] = MultiplayerManager.controlType.Gamepad4;
            } else {
                Debug.LogError("Control type does not exist / is not implemented");
            }
        } else {
            Debug.LogError("Player number does not exist");
        }
    }

    private void Start() {
        multiplayerManager = FindObjectOfType<MultiplayerManager>();

        // Set the options currently selected to whatever is in the multiplayer manager!

    }
}
