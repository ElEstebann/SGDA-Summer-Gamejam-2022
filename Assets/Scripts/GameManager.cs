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
    public static event Game OnUnpause;
    void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            PlayerMovement PM = players[i].GetComponent<PlayerMovement>();
            PM.controlType = MultiplayerManager.instance.playerControls[i];
            PM.hue = MultiplayerManager.instance.playerColors[i];
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

    public void Begin()
    {
        if(OnUnpause != null)
            StartCoroutine(DelayedStart());

    }
    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.25f);
        OnUnpause();
        Debug.Log("BEGIN!");
    }


}
