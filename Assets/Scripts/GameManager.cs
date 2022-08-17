using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] players;

    public delegate void Game();
    public static event Game OnReviveAll;
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            PlayerMovement PM = players[i].GetComponent<PlayerMovement>();
            PM.controlType = MultiplayerManager.instance.playerControls[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire3"))
            {
                if(OnReviveAll != null)
                    OnReviveAll();
            }
    }


}
