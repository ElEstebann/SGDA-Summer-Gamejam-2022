using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possessable : MonoBehaviour
{
    public GameObject possessText;
    public int numInside = 0;
    public bool reserved = false;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    public int maxPlayersIn = 3;
    //public Color hue;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        HideText();
        rb = transform.parent.transform.GetComponent<Rigidbody2D>();
        sprite = transform.parent.transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            numInside++;
            if(numInside > 3)
                numInside = 3;
            DisplayText();
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            numInside--;
            if(numInside < 0)
            {
                numInside = 0;
            }
            if(numInside == 0)
            {
                HideText();
            }

        }
    }

    private void DisplayText()
    {
        if(possessText && numInside>0 && !reserved)
        {
            possessText.SetActive(true);
        }
        else
        {
            HideText();
        }
    }

    private void HideText()
    {
        if(possessText)
        {
            possessText.SetActive(false);
        }
    }

    public void HandleMovement(Vector2 input)
    {
        //Debug.Log("player:" + playerIndex + " " + input);
        rb.AddForce(input*speed, ForceMode2D.Impulse);

    }

    public void Reserve(Color color)
    {
        reserved = true;
        HideText();
        sprite.color = color;
    }

    public void Unreserve()
    {
        reserved = false;
        DisplayText();
        sprite.color = Color.white;
    }
}
