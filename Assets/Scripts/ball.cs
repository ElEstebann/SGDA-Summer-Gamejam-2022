using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {

        switch(collision.gameObject.tag)
        {
            case "Player":
                Debug.Log("Ball hit by " + collision.gameObject.tag);
                transform.SetParent(collision.gameObject.transform);
                rigidbody.isKinematic = true; 
                rigidbody.simulated = false;; 
                break;
            default:
                break;

        }
    }
}
