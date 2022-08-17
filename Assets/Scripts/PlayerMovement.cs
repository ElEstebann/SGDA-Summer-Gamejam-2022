using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private Possessable posessable; 
    private Vector2 direction = Vector2.zero;
    public MultiplayerManager.controlType controlType;
    private bool shot = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        Debug.Log("PLayer" + player.playerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        player.HandleMovement(direction);
        if(Input.GetButtonDown("Fire1"))
        {
            player.ThrowBall();;
        }  

        switch(controlType)
        {
            case MultiplayerManager.controlType.Keyboard1:
                direction.x = Input.GetAxis("Key1 Horizontal");
                direction.y = Input.GetAxis("Key1 Vertical");
                if(Input.GetButtonDown("Key1 Fire"))
                {
                    shot = true;
                }  
                break;

            case MultiplayerManager.controlType.Keyboard2:
                direction.x = Input.GetAxis("Key2 Horizontal");
                direction.y = Input.GetAxis("Key2 Vertical");
                if(Input.GetButtonDown("Key2 Fire"))
                {
                    shot = true;
                }  
                break;

            case MultiplayerManager.controlType.Keyboard3:
                direction.x = Input.GetAxis("Key3 Horizontal");
                direction.y = Input.GetAxis("Key3 Vertical");
                if(Input.GetButtonDown("Key3 Fire"))
                {
                    shot = true;
                }  
                break;

            case MultiplayerManager.controlType.Keyboard4:
                direction.x = Input.GetAxis("Key4 Horizontal");
                direction.y = Input.GetAxis("Key4 Vertical");
                if(Input.GetButtonDown("Key4 Fire"))
                {
                    shot = true;
                }  
                break;

            default:
                break;
        }

        player.HandleMovement(direction);
        if(shot)
        {
            player.ThrowBall();
            shot = false;
        }  
    }
}