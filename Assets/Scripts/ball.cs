using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private bool canBeCaught = true;
    private SpriteRenderer sprite;
    private Collider2D collider;
    [SerializeField] 
    private float pickupSpeed;
    public int owner = 0;
    public float pickupDelay;
    private float currentDelay;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(!canBeCaught && Mathf.Abs(rigidbody.velocity.magnitude) < pickupSpeed && catchDelay <= 0)
        {
            EnableBall();
        }
        if(catchDelay > 0)
        {
            catchDelay -= Time.deltaTime;
        }
        */
        
    }
    
    void FixedUpdate()
    {
        Debug.Log(canBeCaught);
        if(!canBeCaught && Mathf.Abs(rigidbody.velocity.magnitude) < pickupSpeed && currentDelay <= 0)
        {
            EnableBall();
        }
        if(currentDelay> 0)
        {
            currentDelay -= Time.deltaTime;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(owner == 0)
        {
        switch(collision.gameObject.tag)
            {
                case "Player":
                    Debug.Log("Ball hit by " + collision.gameObject.tag);
                    if(canBeCaught)
                    {
                        transform.SetParent(collision.gameObject.transform);
                        PickupBall();
                        PlayerMovement player = transform.parent.GetComponent<PlayerMovement>();
                        player.GetBall();
                        owner = player.playerIndex;

                    }
                    else
                    {

                    }

                    break;
                case "Arena":
                    break;

                default:
                    break;

            }
        }
    }

    void ThrowTo(Vector2 force)
    {
        Debug.Log("Trown!");
        transform.SetParent(GameObject.Find("Ball").transform);
        ReleaseBall();

        rigidbody.AddForce(force);
        sprite.color = Color.red;
        canBeCaught = false;
        
    }

    private void PickupBall()
    {
        collider.enabled = false;
        rigidbody.isKinematic = true; 
        rigidbody.simulated = false;
        rigidbody.velocity = Vector2.zero;
    }

    private void ReleaseBall()
    {
        rigidbody.isKinematic = false; 
        rigidbody.simulated = true;
        collider.enabled = true;
        currentDelay = pickupDelay;
    }

    private void EnableBall()
    {
        sprite.color = Color.white;
        canBeCaught = true;
        owner = 0;
    }
}
