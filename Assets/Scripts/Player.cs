using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerIndex;
    [SerializeField] 
    private float speed;
    [SerializeField] 
    private float throwForce;

    public Vector2 direction = Vector2.zero;
    private bool frozen;
    private Rigidbody2D rb;
    public bool hasBall = false;
  
    private float currentSpeed = 0;
    private float rotation;
    [SerializeField]
    private Animator animator;
    public bool alive = true;
    private Collider2D collider;
    private bool hiding = false;
    private GameObject hideObject;
    public Color hue;
    private GameManager GM;
    public bool hidden = false;
    public Transform rotationJoint;
    public Transform ballPlacement;
    
    private Vector3 originalPosition;
    private SpriteRenderer backing;
    private SpriteRenderer arrow;
    public SpriteRenderer aliveSprite;
    private SpriteRenderer deadSprite;
    public bool stunned = false;
    [SerializeField]
    private float dashModifier = 1.5f;
    public bool dashing = false;
    [SerializeField]
    private float dashImmunity = 1f;
    [SerializeField]
    private float deathProtection = 1f;

    private float currentDashImmunity = 0f;
    private float currentDeathProtection = 0f;


    public MultiplayerManager.characterType characterType= MultiplayerManager.characterType.Clown;
    private Animator timer;
    [SerializeField]
    static int pickupTime = 3;


    // Start is called before the first frame update
    void Start()
    {
        frozen = false;
        rb = GetComponent<Rigidbody2D>();
        if(playerIndex <= 0)
            frozen = true;

        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        originalPosition = transform.position;


        GameManager.OnReviveAll +=Revive;
        hideObject = null;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        rotationJoint = transform.Find("Joint").transform;
        ballPlacement = rotationJoint.Find("BallPlacement").transform;
        arrow = rotationJoint.Find("Arrow").transform.GetComponent<SpriteRenderer>();
        backing = transform.Find("Body").transform.GetComponent<SpriteRenderer>();
        aliveSprite = transform.Find("AliveSprite").GetComponent<SpriteRenderer>();
        deadSprite = transform.Find("DeadSprite").GetComponent<SpriteRenderer>();


        arrow.color = new Color(hue.r,hue.g,hue.b,0f);
        backing.color = new Color(hue.r,hue.g,hue.b,0.5f);

        GameManager.OnBallTimeout += Timeout;
        timer = rotationJoint.Find("Timer").transform.GetComponent<Animator>();

    }

    void OnDestroy()
    {
        GameManager.OnBallTimeout -= Timeout;
        GameManager.OnReviveAll -=Revive;
    }

    // Update is called once per frame
    void Update()
    {    
        if(hiding)
        {
            transform.position = hideObject.transform.position;
        }
        rotationJoint.rotation = Quaternion.Euler(0, 0, rotation);

        
        if(currentDeathProtection > 0)
        {
            currentDeathProtection -= Time.deltaTime;
        }
        if(currentDashImmunity > 0)
        {
            currentDashImmunity -= Time.deltaTime;
        }

        animator.SetFloat("DeathProtection",currentDeathProtection);
        animator.SetFloat("DashImmunity",currentDashImmunity);

    }

    private void FixedUpdate() // using fixed update instead of Update to decrease jitter
    {
        //rb.rotation = rotation;
        if(!hasBall)
        {
            if(dashing)
            {
                rb.velocity = direction*speed * dashModifier;
            }
            else if(!stunned)
            {
                rb.velocity = direction*currentSpeed;
            }

            animator.SetFloat("MoveSpeed",currentSpeed);
            if(direction.x <= 0)
            {
                aliveSprite.flipX = false;
                deadSprite.flipX = false;
            }
            else
            {
                aliveSprite.flipX = true;
                deadSprite.flipX = true;
                
            } 
        }
        else
        {
            animator.SetFloat("MoveSpeed",0f);
        }


    }

    public void ThrowBall()
    {
        if(hasBall)
        {
            hasBall = false;
            //Debug.Log("Ball Thrown");
            BroadcastMessage("ThrowTo",direction*throwForce);
            //Debug.Log("Throwing: " + direction + " " + throwForce);
            animator.SetBool("HasBall",hasBall);
            arrow.color = new Color(hue.r,hue.g,hue.b,0f);
            timer.SetInteger("Time",-1);
        }
        else
        {
            Dash();
        }
    }
    public void DropBall()
    {
        if(hasBall)
        {
            timer.SetInteger("Time",-1);
            hasBall = false;
            BroadcastMessage("ThrowTo",Vector2.zero);
            arrow.color = new Color(hue.r,hue.g,hue.b,0f);
        }
    }

    public void Dash()
    {
        if(alive && !stunned && !dashing)
        {
        dashing = true;
        animator.SetTrigger("Dash");
        }
    }

    public void EndDash()
    {  
        if(dashing)
        {
            Debug.Log("EndDash");
            dashing = false;
            currentSpeed =0;
        }
    }
    public void GetBall()
    {
        hasBall = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("HasBall",hasBall);
        arrow.color = new Color(hue.r,hue.g,hue.b,1f);
        timer.SetInteger("Time",pickupTime);
    }

    public void Die(Ball ball)
    {
        if(alive)
        {
            //Debug.Log("I DIED");
            GM.PlayerKilled(ball.owner,playerIndex);
            PlayHurtsound();
            frozen = false;
            animator.SetBool("Alive",false);
            alive = false;
            //collider.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Ghost");
            ResetProtections();
        }
        

    }

    public void ResetProtections()
    {
        currentDashImmunity = 0f;
        currentDeathProtection = 0f;
    }

    public void Revive()
    {
        if(!alive)
        {
            
            animator.SetBool("Alive",true);
            alive = true;

            FindReviveLocation();
            gameObject.layer = LayerMask.NameToLayer("Default");
            currentDeathProtection = deathProtection;
            
        }
    }

    public void FindReviveLocation()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction,Mathf.Infinity,LayerMask.GetMask("Default"));
        float mod = 0f;
        
        while(hit && hit.distance < 0.6f && hit.collider.tag == "Possessable")
        {
            Debug.Log(hit.collider.tag);
            mod += 0.1f;
            hit = Physics2D.Raycast(transform.position + new Vector3(direction.x*mod,direction.y*mod,0f),direction,Mathf.Infinity,LayerMask.GetMask("Default"));
            
        }
        Vector3 newPos = transform.position + new Vector3(direction.x*mod,direction.y*mod,0f);
        Debug.DrawRay(transform.position,new Vector3(direction.x*mod,direction.y*mod,0f),hue);
        transform.position = newPos;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        switch(collision.gameObject.tag)
        {
            case "Ball":
                Ball ball = collision.gameObject.transform.GetComponent<Ball>();

                if(ball && ball.owner != 0 && ball.owner != playerIndex && currentDeathProtection <= 0)
                {
                    Die(ball);
                }
                else if(ball && !hasBall && ball.owner == playerIndex)
                {
                    Knockback();
                    Debug.Log("Player" + playerIndex + " hit by own ball" + ball.owner);
                    
                }
                break;

            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        switch(collide.gameObject.tag)
        {
            case "Attack":
                if(currentDashImmunity <= 0)
                {
                    Knockback();
                    currentDashImmunity = dashImmunity;
                    Debug.Log("Player" + playerIndex + " hit by dash attack");

                }

                break;


            default:
            break;
        }
    }

    public void Knockback()
    {
        animator.SetTrigger("Stun");
        PlayHurtsound();
        if(hasBall)
        {
            DropBall();
        }
    }

    public void HandleMovement(Vector2 input)
    {
        //Debug.Log("player:" + playerIndex + " " + input);
        if(!stunned)
        {
            if(input == Vector2.zero)
            {
                currentSpeed = 0;
            }
            else
            {
                currentSpeed = speed;
                direction = input;
                direction.Normalize();
                rotation = Mathf.Atan2(direction.y,direction.x)*180/Mathf.PI -90f;

            }

        }
        else
        {
            currentSpeed = -1;
        }
    }

    public void HideAt(GameObject position)
    {
        if(!hidden)
        {
            transform.SetParent(position.transform);
            collider.enabled = false;
            rb.isKinematic = true; 
            rb.simulated = false;
            rb.velocity = Vector2.zero;
            animator.SetBool("Hidden",true);
            hidden = true;
        }
    }

    public void Unhide()
    {
        if(hidden)
        {
            transform.parent =  null;
            collider.enabled = true;
            rb.isKinematic = false; 
            rb.simulated = true;
            animator.SetBool("Hidden",false);
            hidden = false;
            transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
    }

    public void Reset()
    {
        if(hidden)
            Unhide();
        if(!alive)
            Revive();
        hasBall = false;
        transform.position = originalPosition;
        rotation = 0;
        rb.angularVelocity = 0f;
        rb.velocity = new Vector2(0f,0f);
        direction = new Vector2(0f,0f);
        animator.SetTrigger("Reset");
        rotationJoint.rotation = Quaternion.Euler(0, 0, 0);
        ResetProtections();
        arrow.color = new Color(hue.r,hue.g,hue.b,0f);
        ResetAnimator();

    }
    public void ResetAnimator()
    {
        animator.SetBool("HasBall",false);
        animator.SetBool("Alive",true);

  
    }

    public void PlayHurtsound()
    {
        AudioManager.instance.Play("Hit");
        switch(characterType)
        {
            case MultiplayerManager.characterType.Clown:
                AudioManager.instance.Play("ClownHit");
                break;
            
            case MultiplayerManager.characterType.Imp:
                AudioManager.instance.Play("ImpHit");
                break;
            
            case MultiplayerManager.characterType.Dummy:
                AudioManager.instance.Play("DummyHit");
                break;
            
            case MultiplayerManager.characterType.Werewolf:
                AudioManager.instance.Play("WerewolfHit");
                break;
            
            default:
                break;
        }
    }

    public void Timeout()
    {
        if(hasBall)
        {
            Debug.Log("Player" + playerIndex + " timed out!");
            ThrowBall();
            Knockback();
        }

    }




    
}