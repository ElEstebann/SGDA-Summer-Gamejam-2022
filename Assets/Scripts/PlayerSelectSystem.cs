using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image player1CharacterDisplay;
    public Image player2CharacterDisplay;
    public Image player3CharacterDisplay;
    public Image player4CharacterDisplay;

    public int player1CurrentSpriteDisplayed;
    public int player2CurrentSpriteDisplayed;
    public int player3CurrentSpriteDisplayed;
    public int player4CurrentSpriteDisplayed;

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

    public void ScrollCharacterDisplayLeft(int player) {
        if (player == 0) {
            player1CurrentSpriteDisplayed -= 1;
            if (player1CurrentSpriteDisplayed < 0) {
                player1CurrentSpriteDisplayed = 3;
            }
            player1CharacterDisplay.sprite = player1Sprites[player1CurrentSpriteDisplayed];
        } else if (player == 1) {
            player2CurrentSpriteDisplayed -= 1;
            if (player2CurrentSpriteDisplayed < 0) {
                player2CurrentSpriteDisplayed = 3;
            }
            player2CharacterDisplay.sprite = player2Sprites[player2CurrentSpriteDisplayed];
        } else if (player == 2) {
            player3CurrentSpriteDisplayed -= 1;
            if (player3CurrentSpriteDisplayed < 0) {
                player3CurrentSpriteDisplayed = 3;
            }
            player3CharacterDisplay.sprite = player3Sprites[player3CurrentSpriteDisplayed];
        } else if (player == 3) {
            player4CurrentSpriteDisplayed -= 1;
            if (player4CurrentSpriteDisplayed < 0) {
                player4CurrentSpriteDisplayed = 3;
            }
            player4CharacterDisplay.sprite = player4Sprites[player4CurrentSpriteDisplayed];
        }
    }

    public void ScrollCharacterDisplayRight(int player) {
        if (player == 0) {
            player1CurrentSpriteDisplayed += 1;
            if (player1CurrentSpriteDisplayed > 3) {
                player1CurrentSpriteDisplayed = 0;
            }
            player1CharacterDisplay.sprite = player1Sprites[player1CurrentSpriteDisplayed];
        } else if (player == 1) {
            player2CurrentSpriteDisplayed += 1;
            if (player2CurrentSpriteDisplayed > 3) {
                player2CurrentSpriteDisplayed = 0;
            }
            player2CharacterDisplay.sprite = player2Sprites[player2CurrentSpriteDisplayed];
        } else if (player == 2) {
            player3CurrentSpriteDisplayed += 1;
            if (player3CurrentSpriteDisplayed > 3) {
                player3CurrentSpriteDisplayed = 0;
            }
            player3CharacterDisplay.sprite = player3Sprites[player3CurrentSpriteDisplayed];
        } else if (player == 3) {
            player4CurrentSpriteDisplayed += 1;
            if (player4CurrentSpriteDisplayed > 3) {
                player4CurrentSpriteDisplayed = 0;
            }
            player4CharacterDisplay.sprite = player4Sprites[player4CurrentSpriteDisplayed];
        }
    }

    private void Start() {
        multiplayerManager = FindObjectOfType<MultiplayerManager>();

        player1CurrentSpriteDisplayed = 0;
        player2CurrentSpriteDisplayed = 0;
        player3CurrentSpriteDisplayed = 0;
        player4CurrentSpriteDisplayed = 0;

        // Set the options currently selected to whatever is in the multiplayer manager!



    }
}
