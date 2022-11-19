using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  InputHandler inputHandler;
  Animator anim;
  CameraHandler cameraHandler;
  PlayerLocomotion playerLocomotion;

  [Header("Player Flags")]
  public bool isInteracting;
  public bool isSprinting;

  private void Awake()
  {
    cameraHandler = CameraHandler.singleton;
  }

  void Start()
  {
    inputHandler = GetComponent<InputHandler>();
    anim = GetComponentInChildren<Animator>();
    playerLocomotion = GetComponent<PlayerLocomotion>();
  }

  void Update()
  {
    float delta = Time.deltaTime;
    isInteracting = anim.GetBool("isInteracting");
    
    inputHandler.TickInput(delta);
    playerLocomotion.HandleMovement(delta);
    playerLocomotion.HandleDodgingAndSprinting(delta);
  }

  private void FixedUpdate()
  {
    float delta = Time.fixedDeltaTime;

    if(cameraHandler != null)
    {
      cameraHandler.FollowTarget(delta);
      cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
    }
  }

  private void LateUpdate()
  {
    inputHandler.dodgeFlag = false;
    inputHandler.sprintFlag = false;
    isSprinting = inputHandler.b_Input;
  }
}
