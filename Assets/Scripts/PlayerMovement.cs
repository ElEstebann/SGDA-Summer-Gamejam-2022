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
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        player.HandleMovement(direction);
        if(Input.GetButtonDown("Fire1"))
        {
            player.ThrowBall();;
        }  
    }
}