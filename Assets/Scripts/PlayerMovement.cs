using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
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
    private int CPUCounter;
    private Ball ball;
    private GameManager GM;


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
        GameManager.OnGameRestart += Reset;
        CPUCounter = player.playerIndex;
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void OnDestroy()
    {
        GameManager.OnReviveAll -= Eject;
        GameManager.OnUnpause -= Unfreeze;
        GameManager.OnGameOver -= Freeze;
        GameManager.OnGameRestart -= Reset;
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
                    //CPULogic();
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
                case MultiplayerManager.controlType.CPU:
                    if(CPUCounter < 0)
                    {
                        CPULogic();
                        CPUCounter = 5;
                    }
                    else
                    {
                        CPUCounter--;
                    }
                    
                    break;
                default:
                    break;
            }

            HandleInput(direction,shot);
        }
        else
        {
            HandleInput(new Vector2(0,0),false);
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
                    if(possessable && !possessable.reserved)
                    {
                        canPossess = true;
                    }
                    else
                    {
                        possessable = null;
                    }
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
        if(possessable && canPossess && !possessable.reserved)
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
            //player.FindReviveLocation();
            
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

    public void Reset()
    {
        if(isPossessing)
        {
            Eject();
        }
        frozen = true;
        shot = false;
        canPossess = false;
        isPossessing = false;
        player.Reset();


    }

    private void CPULogic()
    {
        if(!player.alive)
        {
            
            if(isPossessing)
            {
                //Path while possessing
                direction = PathTo(GM.players[GM.killerPlayer-1].transform.position);
            }
            else if(canPossess)
            {
                shot = true;
                direction = Vector2.zero;
            }
            else
            {
                //Path to possessable
                GameObject[] gos = GameObject.FindGameObjectsWithTag("Possessable");
                
                if(gos.Length >0)
                {
                    Vector3 shortestDistance =gos[0].transform.position;
                    for(int i = 1; i < gos.Length; i++)
                    {
                        if((gos[i].transform.position - transform.position).magnitude < (shortestDistance - transform.position).magnitude)
                        {
                            shortestDistance = gos[i].transform.position;
                        }
                        
                    }
                    //Debug.DrawRay(transform.position,shortestDistance,hue);
                    direction = PathTo(shortestDistance);
                }
                else
                {
                    direction = Vector2.zero;
                }
                
            }
        }
        else if (ball.owner != player.playerIndex && ball.owner != 0)
        {
            Debug.Log("Run away");
            direction = PathTo((transform.position-ball.transform.position)*10f + transform.position);
        }
        else if(player.hasBall)
        {
            //Debug.Log("Aim at player");
            Vector2 lowestDistance = Vector2.zero;
            float minDistance = 999f;
            for(int i = 0; i < 4; i++)
            {
                if(i != player.playerIndex - 1 )
                {
                    Vector3 target = GM.players[i].transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(Vector3.Normalize(target - transform.position)*1.5f + transform.position, target - transform.position,Mathf.Infinity,LayerMask.GetMask("Default"));
                    Debug.DrawRay(Vector3.Normalize(target - transform.position)*1.5f + transform.position,GM.players[i].transform.position - transform.position,hue);
                    //Debug.Log(hit.collider.tag);
                    if(hit.collider && hit.collider.tag == "Player" && lowestDistance.Equals(Vector2.zero))
                    {
                        
                        lowestDistance = hit.collider.transform.position;
                        Debug.Log("SET: " + lowestDistance);
                    }
                    else if(hit.collider && hit.collider.tag == "Player" && minDistance < (hit.collider.transform.position - transform.position).magnitude)
                    {
                        
                        lowestDistance = hit.collider.transform.position;
                        Debug.Log("SET: " + lowestDistance);
                    }
                }
                
            }
            //direction.x = lowestDistance.x-transform.position.x;
            //irection.y = lowestDistance.y - transform.position.y;
            direction = PathTo(lowestDistance);
            

            //Debug.DrawRay(transform.position,direction,hue);
            
            
        }
        else if(ball.owner == player.playerIndex)
        {
            direction = PathTo(Vector3.Normalize(transform.position - ball.transform.position)*1.5f + ball.transform.position);
        }
        else
        {
            Debug.Log("Path to ball");
            direction = PathTo(ball.transform.position);
        }
        
    }

    public Vector2 PathTo(Vector3 coord)
    {
        Vector2 reto = Vector2.zero;

        reto.x = coord.x-transform.position.x;
        reto.y = coord.y - transform.position.y;
        
        Debug.DrawRay(transform.position,reto,hue);
        return reto;
    }

    public Vector2 PathToBall(Vector3 target)
    {
        RaycastHit2D hit = Physics2D.Raycast(Vector3.Normalize(target - transform.position)*1.5f + transform.position, target - transform.position,Mathf.Infinity,LayerMask.GetMask("Default"));
        if(hit.collider && hit.collider.tag == "Ball")
        {        
            return PathTo(target);
        }
        else
        {
            Vector3 perpendicular = Vector2.Perpendicular(target - transform.position);
            perpendicular.Normalize();
            RaycastHit2D hitUp = Physics2D.Raycast(perpendicular*1.5f + transform.position, perpendicular,Mathf.Infinity,LayerMask.GetMask("Default"));
            RaycastHit2D hitDown = Physics2D.Raycast((-perpendicular)*1.5f + transform.position, -perpendicular,Mathf.Infinity,LayerMask.GetMask("Default"));
            Vector2 lowestDistance = Vector2.zero;
            if(hitUp.collider && hitUp.collider.tag != "Possessable")
            {
                lowestDistance = hitUp.collider.transform.position;
            }
            if(hitDown.collider && hitDown.collider.tag != "Possessable" && (hitDown.collider.transform.position - target).magnitude < (new Vector3(lowestDistance.x,lowestDistance.y,0f)-target).magnitude)
            {
                lowestDistance = hitDown.collider.transform.position;
            }
            if(lowestDistance == Vector2.zero && hitUp.collider)
            {
                lowestDistance = hitUp.collider.transform.position;
            }
            else if(lowestDistance == Vector2.zero && hitDown.collider)
            {
                lowestDistance = hitDown.collider.transform.position;
            }
            return PathTo(lowestDistance);
        }
    }

}