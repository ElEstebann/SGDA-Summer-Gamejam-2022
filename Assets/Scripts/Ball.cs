using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canBeCaught = true;
    private SpriteRenderer sprite;
    private Collider2D collider;
    [SerializeField] 
    private float pickupSpeed;
    public int owner = 0;
    public float pickupDelay;
    private float currentDelay;
    private Color hue;
    private TrailRenderer trail;
    private Vector3 originalPosition;
    private float originalRotation;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        hue = Color.blue;
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
        originalPosition = transform.position;
        originalRotation = rb.rotation;
        GameManager.OnGameRestart += Reset;

    }

    void OnDestroy()
    {
        GameManager.OnGameRestart -= Reset;
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
        //Debug.Log(canBeCaught);
        if(!canBeCaught && Mathf.Abs(rb.velocity.magnitude) < pickupSpeed && currentDelay <= 0)
        {
            EnableBall();
        }
        if(currentDelay> 0)
        {
            currentDelay -= Time.deltaTime;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(owner == 0)
        {
        switch(collision.gameObject.tag)
            {
                case "Player":
                    //Debug.Log("Ball hit by " + collision.gameObject.tag);
                    if(canBeCaught)
                    {
                        //transform.SetParent(collision.gameObject.transform);
                        Player player = collision.gameObject.transform.GetComponent<Player>();
                        if(!player.stunned || player.dashing)
                        {
                            PickupBall();
                            transform.SetParent(player.ballPlacement);
                            transform.localPosition = Vector3.zero;
                            
                            player.GetBall();
                            owner = player.playerIndex;
                            hue = player.hue;
                        }

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

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(owner == 0)
        {
        AudioManager.instance.Play("BallHit");
        switch(collision.gameObject.tag)
            {
                case "Player":
                    //Debug.Log("Ball hit by " + collision.gameObject.tag);
                    if(canBeCaught)
                    {
                        //transform.SetParent(collision.gameObject.transform);
                        Player player = collision.gameObject.transform.GetComponent<Player>();
                        if(!player.stunned || player.dashing)
                        {
                            PickupBall();
                            transform.SetParent(player.ballPlacement);
                            //transform.rotation = Quaternion.Euler(0f,0f,0f);
                            transform.localPosition = Vector3.zero;
                            
                            player.GetBall();
                            owner = player.playerIndex;
                            hue = player.hue;
                        }

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
        //Debug.Log("Trown!" + force);
        transform.SetParent(GameObject.Find("Ball").transform);
        ReleaseBall();
        if(force != Vector2.zero)
        {
        AudioManager.instance.Play("BallThrow");
        rb.AddForce(force);
        sprite.color = hue;
        trail.enabled = true;
        trail.startColor = hue;
        trail.endColor = hue;
        }
        else
        {
            owner = 0;
        }
        canBeCaught = false;
        
        
    }

    private void PickupBall()
    {
        AudioManager.instance.Play("BallPickUp");
        collider.enabled = false;
        rb.isKinematic = true; 
        rb.simulated = false;
        rb.velocity = Vector2.zero;
    }

    private void ReleaseBall()
    {
        rb.isKinematic = false; 
        rb.simulated = true;
        collider.enabled = true;
        currentDelay = pickupDelay;
    }

    private void EnableBall()
    {
        sprite.color = Color.white;
        canBeCaught = true;
        owner = 0;
        trail.enabled = false;
    }

    void Reset()
    {
        if(owner!=0)
        {
            ThrowTo(Vector2.zero);
            EnableBall();
        }
        
        rb.angularVelocity = 0;
        rb.velocity = new Vector2(0f,0f);
        
        transform.position = originalPosition;
    }
}
