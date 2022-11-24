using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
  PlayerManager playerManager;
  public Animator anim;
  InputHandler inputHandler;
  PlayerLocomotion playerLocomotion;
  int vertical;
  int horizontal;
  public bool canRotate;

  public void Initialize()
  {
    playerManager = GetComponentInParent<PlayerManager>();
    anim = GetComponent<Animator>();
    inputHandler = GetComponentInParent<InputHandler>();
    playerLocomotion = GetComponentInParent<PlayerLocomotion>();
    vertical = Animator.StringToHash("Vertical");
    horizontal = Animator.StringToHash("Horizontal");          
  }
        
  public void UpdateAnimatorValues(float vertMovement, float horzMovement, bool isSprinting)
  {
    #region Vertical

    float v = 0;

    if(vertMovement > 0 && vertMovement < 0.55f)
    {
      v = 0.5f;
    }

    else if( vertMovement > 0.55f)
    {
      v = 1f;        
    }
    else if(vertMovement < 0 && vertMovement > -0.55f)
    {
      v = -0.5f;
    }
    else if(vertMovement < -0.55f)
    {
      v = -1f;
    }
    else
    {
      v = 0;
    }
    #endregion

    #region Horizontal

    float h = 0;

    if(horzMovement > 0 && horzMovement < 0.55f)
    {
      h = 0.5f;
    }

    else if( horzMovement > 0.55f)
    {
      h = 1f;        
    }
    else if(horzMovement < 0 && horzMovement > -0.55f)
    {
      h = -0.5f;
    }
    else if(horzMovement < -0.55f)
    {
      h = -1f;
    }
    else
    {
      h = 0;
    }
    #endregion

    if(isSprinting)
    {
      v = 2;
      h = horzMovement;
    }

    anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
    anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);

  }

  public void PlayTargetAnimation(string targetAnim, bool isInteracting)
  {
    anim.applyRootMotion = isInteracting;
    
    anim.SetBool("isInteracting", isInteracting);
    anim.CrossFade(targetAnim, 0.2f);
  }

  public void CanRotate()
  {
    canRotate = true;
  }

  public void StopRotation()
  {
    canRotate = false;
  }

  private void OnAnimatorMove()
  {
    if (!playerManager.isInteracting)
      return;

    float delta = Time.deltaTime;
    playerLocomotion.rigidbody.drag = 0;
    Vector3 deltaPosition = anim.deltaPosition;
    deltaPosition.y = 0;
    Vector3 velocity = deltaPosition / delta;
    playerLocomotion.rigidbody.velocity = velocity;
  }
}