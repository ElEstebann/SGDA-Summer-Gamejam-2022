using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleText : MonoBehaviour
{
    private GameManager GM;
    private Animator animator;
    private Player winner;

    
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        GameManager.OnGameOver += GameOverScreen;
        GameManager.OnGameRestart += Restart;


    }

    void OnDestroy()
    {
        GameManager.OnGameOver -= GameOverScreen;
        GameManager.OnGameRestart -= Restart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOverScreen()
    {
        animator.SetBool("GameOver",true);


    }
    void Restart()
    {
        animator.SetTrigger("Restart");
        animator.SetBool("GameOver",false);
    }

    public void PlayCountDown()
    {
        GM.PlayCountDown();
    }
}
