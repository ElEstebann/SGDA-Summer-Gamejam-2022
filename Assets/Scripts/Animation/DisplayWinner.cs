using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayWinner : StateMachineBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField]
    private int number;
    private GameManager GM;
    [SerializeField]
    private Color32 hue;
    [SerializeField]
    private int fontSize = 350;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        text = animator.gameObject.transform.Find("Countdown").GetComponent<TextMeshProUGUI>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        number = GM.roundWinner.playerIndex;
        hue = GM.roundWinner.hue;
        text.fontSize = fontSize;
        text.text = "<color=#" + ColorUtility.ToHtmlStringRGB(hue) + ">PLAYER " + number + "\nWINS!</color>";
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.instance.Play("Results");    
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
