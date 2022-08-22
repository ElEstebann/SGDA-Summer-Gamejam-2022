using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : StateMachineBehaviour
{
    TextMeshProUGUI text;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        text = animator.transform.Find("Time").GetComponent<TextMeshProUGUI>();
        if(animator.GetInteger("Time") > 0)
            text.text = animator.GetInteger("Time").ToString();
        
        else
        {
            text.text = "";
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            GM.BallTimeout();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
       animator.SetInteger("Time",animator.GetInteger("Time")-1);
       if(animator.GetInteger("Time") <= animator.GetInteger("WarningTime"))
        {
            animator.SetBool("Warning",true);
        }
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
