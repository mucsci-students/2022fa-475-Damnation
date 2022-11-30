using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool: StateMachineBehaviour
{
  public string targetBool;
  public bool status;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    //animator.SetBool("isInteracting", status);
    animator.SetBool(targetBool, status);
  }

  /*
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animator.SetBool("isInteracting", false);
  }
  */
}
