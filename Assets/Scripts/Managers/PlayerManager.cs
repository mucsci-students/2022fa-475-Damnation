using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  InputHandler inputHandler;
  Animator anim;
  CameraHandler cameraHandler;
  public PlayerLocomotion playerLocomotion;
  EstusFlasks estus;


  [Header("Player Flags")]
  public bool isInteracting;
  public bool isSprinting;
  public bool isInAir;
  public bool isGrounded;
  public bool isUsingRightHand;
  public bool isUsingLeftHand;
  public bool invincibilityFlag;
  public bool healFlag;
  public bool outOfStamina;

  [Header("I-Frame Data")]
  public GameObject iFrameCube;
  public float iFrameTimer = 0f;
  public float maxIFrameTimer = 0.1f;

  [Header("Heal Data")]
  public bool healTimerEnabled;
  public float healTimer = 0f;
  public float maxHealTimer = 10f;
  public GameObject healSound;

  private void Awake()
  {
    cameraHandler = CameraHandler.singleton;
  }

  void Start()
  {
    inputHandler = GetComponent<InputHandler>();
    anim = GetComponentInChildren<Animator>();
    playerLocomotion = GetComponent<PlayerLocomotion>();
    estus = GetComponent<EstusFlasks>();
    //animHandler = GetComponentInChildren<AnimationHandler>();
  }

  void Update()
  {
    float delta = Time.deltaTime;
    isInteracting = anim.GetBool("isInteracting");
    isUsingRightHand = anim.GetBool("isUsingRightHand");
    isUsingLeftHand = anim.GetBool("isUsingLeftHand");
    
    inputHandler.TickInput(delta);
    playerLocomotion.HandleMovement(delta);
    playerLocomotion.HandleDodgingAndSprinting(delta);
    playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
    CheckFlask(delta);
    dodgeIFrames(delta);
    
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
    inputHandler.rb_Input = false;
    inputHandler.rt_Input = false;
    inputHandler.x_Input = false;
    
    if(isInAir)
    {
      playerLocomotion.inAirTimer += Time.deltaTime;
    }
  }

  void dodgeIFrames(float delta)
  {
    if(inputHandler.dodgeFlag)
    {
      iFrameCube.SetActive(true);
      invincibilityFlag = true;
    }

    if(invincibilityFlag)
    {
      iFrameTimer += delta;
      if(iFrameTimer >= maxIFrameTimer)
      {
        iFrameCube.SetActive(false);
        invincibilityFlag = false;
      }
    }
    else
    {
      iFrameTimer = 0f;
    }
  }

  void CheckFlask(float delta)
  {
    healFlag = inputHandler.x_Input;

    if(healFlag && !healTimerEnabled)
    { 
      if(estus.flaskCount == 0)
        return;

      healTimerEnabled = true;
      estus.DrinkFlask();
      estus.flaskCount--;
      healSound.SetActive(false);
      healSound.SetActive(true);
      healFlag = false;
    }

    if(healTimerEnabled)
    {
      healTimer += delta;
      if(healTimer >= maxHealTimer)
      {
        healTimerEnabled = false;
      }
    }
    
  }
}
