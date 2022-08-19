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
    private SpriteRenderer aliveSprite;
    private SpriteRenderer deadSprite;
    public bool stunned = false;

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

        arrow.color = new Color(hue.r,hue.g,hue.b,0.5f);
        backing.color = new Color(hue.r,hue.g,hue.b,0.5f);

    }

    // Update is called once per frame
    void Update()
    {    
        if(hiding)
        {
            transform.position = hideObject.transform.position;
        }
        rotationJoint.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void FixedUpdate() // using fixed update instead of Update to decrease jitter
    {
            //rb.rotation = rotation;
            if(!hasBall && !stunned)
            {
                rb.velocity = direction*currentSpeed;
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
        }
    }
    public void GetBall()
    {
        hasBall = true;
        rb.velocity = Vector2.zero;
    }

    public void Die(Ball ball)
    {
        if(alive)
        {
            //Debug.Log("I DIED");
            GM.PlayerKilled(ball.owner,playerIndex);
            frozen = false;
            animator.SetBool("Alive",false);
            alive = false;
            //collider.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Ghost");
        }
        

    }

    public void Revive()
    {
        if(!alive)
        {
            animator.SetBool("Alive",true);
            alive = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        switch(collision.gameObject.tag)
        {
            case "Ball":
                Ball ball = collision.gameObject.transform.GetComponent<Ball>();

                if(ball && ball.owner != 0 && ball.owner != playerIndex)
                {
                    Die(ball);
                }
                else
                {

                }
                break;

            default:
                break;
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
            currentSpeed = 0;
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
    }



    
}