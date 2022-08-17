using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager instance;
    public bool editable = false;

    public enum controlType
    {
        CPU,
        Keyboard1,
        Keyboard2,
        Keyboard3,
        Keyboard4,
        Gamepad1,
        Gamepad2,
        Gamepad3,
        Gamepad4,
    }

    public controlType[] playerControls = new controlType[4];
    
    void Awake()    
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null){
            instance = this;
            Debug.Log("Instance was null");
        } else if (instance != this){
            Destroy(gameObject);
            Debug.Log("Destroyed.");
            return;
        }
        
        for(int i = 0; i<4;i++)
        {
            playerControls[i] = controlType.CPU;
        }
        playerControls[0] = controlType.Keyboard1;
    }

    void Update()
    {
        if(editable)
        {


        }
    }

    

}
