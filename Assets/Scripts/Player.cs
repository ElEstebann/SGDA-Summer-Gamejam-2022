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
    public bool hasBall;
  
    private float currentSpeed = 0;
    private float rotation;
    [SerializeField]
    private Animator animator;
    public bool alive = true;
    private Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        frozen = false;
        rb = GetComponent<Rigidbody2D>();
        if(playerIndex <= 0)
            frozen = true;

        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        GameManager.OnReviveAll +=Revive;
    }

    // Update is called once per frame
    void Update()
    {    
    }

    private void FixedUpdate() // using fixed update instead of Update to decrease jitter
    {
            rb.rotation = rotation;
            rb.velocity = direction*currentSpeed;
    }

    public void ThrowBall()
    {
        if(hasBall)
        {
            hasBall = false;
            //Debug.Log("Ball Thrown");
            BroadcastMessage("ThrowTo",direction*throwForce);
            Debug.Log("Throwing: " + direction + " " + throwForce);
        }
    }
    public void GetBall()
    {
        hasBall = true;
    }

    public void Die()
    {
        if(alive)
        {
            //Debug.Log("I DIED");
            frozen = false;
            animator.SetTrigger("Killed");
            alive = false;
            //collider.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Ghost");
        }
        

    }

    public void Revive()
    {
        if(!alive)
        {
            animator.SetTrigger("Revived");
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
                    Die();
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
        Debug.Log("player:" + playerIndex + " " + input);
        if(!frozen)
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
                rotation = rotation = Mathf.Atan2(direction.y,direction.x)*180/Mathf.PI -90f;

            }

        }
    }

    
}