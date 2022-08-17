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
        foreach(GameObject player in players)
        {
            //Setup player controls
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
