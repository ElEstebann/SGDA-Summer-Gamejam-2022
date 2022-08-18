using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private Possessable possessable; 
    private Vector2 direction = Vector2.zero;
    public MultiplayerManager.controlType controlType;
    private bool shot = false;
    private bool canPossess = false;
    [SerializeField]
    private bool isPossessing = false;
    public Color hue;
    [SerializeField]
    private bool frozen = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        player.hue = hue;
        //Debug.Log(hue);
        //Debug.Log("Player" + player.playerIndex + " " + controlType);
        GameManager.OnReviveAll += Eject;
        GameManager.OnUnpause += Unfreeze;
        GameManager.OnGameOver += Freeze;
    }

    void OnDestroy()
    {
        GameManager.OnReviveAll -= Eject;
        GameManager.OnUnpause -= Unfreeze;
        GameManager.OnGameOver -= Freeze;
    }

    // Update is called once per frame
    void Update()
    {
        if(!frozen)
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

            HandleInput(direction,shot);
        }
    }
    void HandleInput(Vector2 dir,bool didShoot)
    {
            if(isPossessing)
            {
                possessable.HandleMovement(dir);
            }
            else
            {
                player.HandleMovement(dir);
            }
            
            if(didShoot)
            {
                if(canPossess)
                {
                    Possess();
                }
                else if(isPossessing)
                {
                    Eject();
                }
                else
                {
                    player.ThrowBall();
                }
                shot = false;
            }  
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        switch(collision.gameObject.tag)
        {
            case "Possessable":
                if(!isPossessing)
                {
                    possessable = collision.gameObject.transform.GetComponent<Possessable>();
                    
                    canPossess = true;
                }
                break;

            default:
                break;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        switch(collision.gameObject.tag)
        {
            case "Possessable":
                if(!isPossessing)
                {
                    possessable = collision.gameObject.transform.GetComponent<Possessable>();
                    
                    canPossess = false;
                    possessable = null;
                }
                break;

            default:
                break;
        }
        
    }

    private void Possess()
    {
        if(possessable && canPossess)
        {
            isPossessing = true;
            canPossess = false;
            possessable.Reserve(hue);
            player.HideAt(possessable.gameObject);
        }
    }

    public void Eject()
    {
        if(isPossessing)
        {
            isPossessing = false;
            canPossess = false;
            possessable.Unreserve();
            player.Unhide();
            possessable = null;
        }
    }

    private void Freeze()
    {
        Debug.Log("FROZEN");
        frozen = true;
        HandleInput(new Vector2(0,0),false);
    }

    private void Unfreeze()
    {
        frozen = false;
    }

}