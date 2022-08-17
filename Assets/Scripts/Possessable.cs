using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possessable : MonoBehaviour
{
    public GameObject possessText;
    public int numInside = 0;
    // Start is called before the first frame update
    void Start()
    {
        HideText();
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
            DisplayText();
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            numInside--;
            if(numInside == 0)
            {
                HideText();
            }
        }
    }

    private void DisplayText()
    {
        if(possessText)
        {
            possessText.SetActive(true);
        }
    }

    private void HideText()
    {
        if(possessText)
        {
            possessText.SetActive(false);
        }
    }
}
