using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerIndex;
    [SerializeField] 
    private float speed;
    [SerializeField] 
    private float throwForce;

    public Vector2 direction;
    private bool frozen;
    [SerializeField]
    private Rigidbody2D rigidbody;
    private bool hasBall;
  
    private float currentSpeed = 0;
    private float rotation;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        frozen = false;
        direction.x = 0;
        direction.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
 
        if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            currentSpeed = 0;
            rigidbody.rotation = rotation;
        }
        else
        {
            currentSpeed = speed;
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");
            direction.Normalize();
            rotation = rotation = Mathf.Atan2(direction.y,direction.x)*180/Mathf.PI -90f;
            rigidbody.rotation = rotation;
        }
        
        rigidbody.velocity = direction*currentSpeed;

        if(Input.GetButtonDown("Fire1") && hasBall)
        {
            ThrowBall();;
        }
        
    }

    private void FixedUpdate() // using fixed update instead of Update to decrease jitter
    {
        if (!frozen) // only let the player move if the player is not fixing something // this variable is set in the animation state
        {
            //transform.Translate(xtrans * Time.fixedDeltaTime, ytrans * Time.fixedDeltaTime, 0);
            

        }
    }

    private void ThrowBall()
    {
        hasBall = false;
        Debug.Log("Ball Thrown");
        BroadcastMessage("ThrowTo",direction*throwForce);
    }
    public void GetBall()
    {
        hasBall = true;
    }

    public void Kill()
    {
        
    }
}
