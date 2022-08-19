using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResultScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public TextMeshProUGUI[] texts;
    public TextMeshProUGUI[] playerNames;
    public Image[] backdrops;
    [SerializeField]
    private Color winColor = Color.white;
    private GameManager GM;
    void Start()
    {
        for(int i = 0;i<4;i++)
        {
            Color hue = MultiplayerManager.instance.playerColors[i];
            playerNames[i].text = "<color=#" + ColorUtility.ToHtmlStringRGB(hue) + ">Player " + (i+1) + "</color>";
            texts[i].text = "<color=#" + ColorUtility.ToHtmlStringRGB(hue) + ">0</color>";
            backdrops[i].color = new Color(1,1,1,0);
        }
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.OnGameOver += UpdateWins;
        UpdateWins();

    }
    void OnDestroy()
    {
        GameManager.OnGameOver -= UpdateWins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateWins()
    {
        Debug.Log("UPDATING");
        for(int i = 0;i<4;i++)
        {
            Color hue = MultiplayerManager.instance.playerColors[i];
            texts[i].text = "<color=#" + ColorUtility.ToHtmlStringRGB(hue) + ">" + MultiplayerManager.instance.wins[i] + "</color>";


            if(GM.roundWinner.playerIndex == i+1)
            {
                backdrops[i].color = winColor;
            }
            else
            {
                backdrops[i].color = new Color(1,1,1,0);
            }
        }
    }
}
