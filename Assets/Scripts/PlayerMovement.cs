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
        Debug.Log("Player" + player.playerIndex + " " + controlType);
    }

    // Update is called once per frame
    void Update()
    {
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

            case MultiplayerManager.controlType.Gamepad1:
                direction.x = Input.GetAxis("Joy1 Horizontal");
                direction.y = Input.GetAxis("Joy1 Vertical");
                if(Input.GetButtonDown("Joy1 Fire"))
                {
                    shot = true;
                }  
                
                break;


            case MultiplayerManager.controlType.Gamepad2:
                direction.x = Input.GetAxis("Joy2 Horizontal");
                direction.y = Input.GetAxis("Joy2 Vertical");
                if(Input.GetButtonDown("Joy2 Fire"))
                {
                    shot = true;
                }  
                break;
                
            case MultiplayerManager.controlType.Gamepad3:
                direction.x = Input.GetAxis("Joy3 Horizontal");
                direction.y = Input.GetAxis("Joy3 Vertical");
                if(Input.GetButtonDown("Joy3 Fire"))
                {
                    shot = true;
                }  
                break;

            case MultiplayerManager.controlType.Gamepad4:
                direction.x = Input.GetAxis("Joy4 Horizontal");
                direction.y = Input.GetAxis("Joy4 Vertical");
                if(Input.GetButtonDown("Joy4 Fire"))
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        /*switch(collision.gameObject.tag)
        {
            case "Ball":
                Ball ball = collision.gameObject.transform.GetComponent<Ball>();

                if(ball && ball.owner != 0 && ball.owner != playerIndex)
                {
                    Die();
                }
                else
                {

                }
                break;

            default:
                break;
        }
        */
    }
}