using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject[] players;

    public delegate void Game();
    public static event Game OnReviveAll;
    public static event Game OnUnpause;
    public static event Game OnGameOver;
    public static event Game OnGameRestart;
    public static event Game OnBallTimeout;
    public Player roundWinner;
    public int numDead = 0;
    public int killerPlayer = 0;
    [SerializeField]
    private GameObject pauseMenu;
    private bool canPause = false;
    private bool paused = false;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button replayButton;
    private bool canCountdown = true;


    
    void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            PlayerMovement PM = players[i].GetComponent<PlayerMovement>();
            PM.controlType = MultiplayerManager.instance.playerControls[i];
            PM.hue = MultiplayerManager.instance.playerColors[i];
            Player PMPlayer = PM.gameObject.transform.GetComponent<Player>();
            PMPlayer.characterType = MultiplayerManager.instance.characters[i];
            pauseMenu = transform.Find("Canvas").gameObject;
            SpriteRenderer SR = PM.gameObject.transform.Find("AliveSprite").GetComponent<SpriteRenderer>();
            SR.sprite = MultiplayerManager.instance.playerSprites[i];
            HidePauseMenu();


            
        }
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Universal Pause"))
            {
                Pause();
            }
    }

    public void Begin()
    {
        if(OnUnpause != null)
            StartCoroutine(DelayedStart());

    }
    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.25f);
        OnUnpause();
        Debug.Log("BEGIN!");
        canPause = true;
        AudioManager.instance.Play("Carnival");
    }

    public void ReviveAll()
    {
        if(OnReviveAll != null)
        {
            OnReviveAll();
            numDead = 0;
            killerPlayer = 0;
        }
    
    }

    public void PlayerKilled(int killer,int dead)
    {
        if(killer == killerPlayer)
        {
            numDead++;
            if(numDead == 3)
            {
                EndGame(killer);
            }
        }
        else
        {
            ReviveAll();
            numDead = 1;
            killerPlayer = killer;
        }
    }

    public void EndGame(int winner)
    {
        Debug.Log("GAME IS OVER. PLAYER " + winner + " WINS!");
        roundWinner = players[winner-1].transform.GetComponent<Player>();
        MultiplayerManager.instance.Win(roundWinner.playerIndex);
        canPause = false;
        replayButton.Select();
        if(OnGameOver != null)
        {
            Debug.Log("ON GAME OVER");
            OnGameOver();
        }
        AudioManager.instance.Stop("Carnival");
        AudioManager.instance.Play("Victory");
        
    }

    public void RestartGame()
    {
        Debug.Log("GAME RESTARTING");
        AudioManager.instance.Stop("Carnival");
        AudioManager.instance.Stop("Results");
        if(paused)
        {
            Unpause();
        }
        if(OnGameRestart != null)
        {
            OnGameRestart();
        }
        killerPlayer = 0;
        numDead = 0;
        roundWinner = null;
        canCountdown = true;
        canPause = false;
    }

    public void Pause()
    {
        if(paused)
        {
            Unpause();
        }
        else if(canPause)
        {
            Time.timeScale = 0f;
            ShowPauseMenu();
            resumeButton.Select();
            paused = true;
        }
        
    }

    public void Unpause()
    {
        if(paused)
        {
            HidePauseMenu();
            Time.timeScale = 1f;
            paused = false;
            if(OnUnpause != null)
            {
                OnUnpause();
            }
        }
    }

    private void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    private void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    
    public void PlayCountDown()
    {
        if(canCountdown)
        {
            AudioManager.instance.Play("321Fight");
            canCountdown = false;
        }
    }

    public void Menu()
    {
        Unpause();
        AudioManager.instance.Stop("Carnival");
        AudioManager.instance.Stop("Results");
        SceneManager.LoadScene(0);
       
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BallTimeout()
    {
        if(OnBallTimeout != null)
            OnBallTimeout();
        //ReviveAll();
        Debug.Log("Ball Timed Out");
        AudioManager.instance.Play("Buzzer");
    }





}
