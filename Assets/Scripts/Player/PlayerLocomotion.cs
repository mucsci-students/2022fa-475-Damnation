using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
  PlayerManager playerManager;
  Transform cameraObject;
  InputHandler inputHandler;
  PlayerStats playerStats;
  [HideInInspector]
  public Vector3 moveDirection;


  [HideInInspector]
  public Transform myTransform;
  //[HideInInspector]
  public AnimationHandler animationHandler;

  public new Rigidbody rigidbody;
  public GameObject normalCamera;

  [Header("Ground & Air Detection Stats")]
  [SerializeField]
  float groundDetectionRayStartPoint = 0.5f; //Distance between Raycast Begin and Ground Detector.
  [SerializeField]
  float minimumDistanceNeededToBeginFall = 1f;
  [SerializeField]
  float groundDirectionRayDistance = 0.2f;
  LayerMask ignoreForGroundCheck;
  public float inAirTimer;
  public bool deathFall;

  [Header("Movement Stats")]
  [SerializeField]
  float walkingSpeed = 3;
  [SerializeField]
  float movementSpeed = 5;
  [SerializeField]
  float sprintSpeed = 7;
  [SerializeField]
  float rotationSpeed = 10;
  [SerializeField]
  float dodgeSpeed = 15;
  [SerializeField]
  float fallingSpeed = 45;
/*
  [Header("Rolling")]
  [SerializeField]
  Transform rollEndpoint;
  [SerializeField]
  Transform rollEndpointTransform;
  [SerializeField]
  Transform backstepEndpointTransform;
  [SerializeField]
  Transform rollCarrier;
*/
  void Start()
  {
    playerManager = GetComponent<PlayerManager>();
    rigidbody = GetComponent<Rigidbody>();
    inputHandler = GetComponent<InputHandler>();
    playerStats = GetComponent<PlayerStats>();
    //animationHandler = GetComponent<AnimationHandler>();
    cameraObject = Camera.main.transform;
    myTransform = transform;
    animationHandler.Initialize();
    playerManager.isGrounded = true;
    ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
  }
  
  #region Movement


  Vector3 normalVector;
  Vector3 targetPosition;

  private void handleRotation(float delta)
  {
    if(playerStats.isDead)
    {
      return;
    }
      
    
    Vector3 targetDir = Vector3.zero;
    float moveOverride = inputHandler.moveAmount;

    targetDir = cameraObject.forward * inputHandler.vertical;
    targetDir += cameraObject.right * inputHandler.horizontal;

    targetDir.Normalize();
    targetDir.y = 0;

    if(targetDir == Vector3.zero)
    {
      targetDir = myTransform.forward;
    }
    
      
    float rs = rotationSpeed;

    Quaternion tr = Quaternion.LookRotation(targetDir);
    Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

    myTransform.rotation = targetRotation;
  }

  public void HandleMovement(float delta)
  {
    if(playerStats.isDead)
      return;

    if (inputHandler.dodgeFlag)
      return;

    if (playerManager.isInteracting)
      return;
      
    moveDirection = cameraObject.forward * inputHandler.vertical;
    moveDirection += cameraObject.right * inputHandler.horizontal;
    moveDirection.Normalize();
    moveDirection.y = 0;
    
    float speed = movementSpeed;

    if(inputHandler.sprintFlag && inputHandler.moveAmount > 0.5)
    {
      speed = sprintSpeed;
      playerManager.isSprinting = true;
      moveDirection *= speed;
    }
    else
    {
      if(inputHandler.moveAmount < 0.5)
      {
        moveDirection *= walkingSpeed;
        playerManager.isSprinting = false;
      }
      else
      {
        moveDirection *= speed;
        playerManager.isSprinting = false;
      }
    }
    

    Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
    rigidbody.velocity = projectedVelocity;

    
    animationHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);
    
    if(animationHandler.canRotate)
    {
      handleRotation(delta);
    }
  }

  public void HandleDodgingAndSprinting(float delta)
  {
    if(playerStats.isDead)
      return;

    if(animationHandler.anim.GetBool("isInteracting"))
      return;

    if(inputHandler.dodgeFlag)
    {
      moveDirection = cameraObject.forward * inputHandler.vertical;
      moveDirection += cameraObject.right * inputHandler.horizontal;

      if(inputHandler.moveAmount > 0)
      {
        playerManager.isInteracting = true;
        animationHandler.PlayTargetAnimation("Roll", true);
        moveDirection.y = 0;
        Quaternion dodgeRotation = Quaternion.LookRotation(moveDirection);
        myTransform.rotation = dodgeRotation;
      }
      else
      {
        playerManager.isInteracting = true;
        animationHandler.PlayTargetAnimation("Backstep Dodge", true);
      }
    }
  }

  public void HandleFalling(float delta, Vector3 moveDirection)
  {
    playerManager.isGrounded = false;
    RaycastHit hit;
    Vector3 origin = myTransform.position;
    origin.y += groundDetectionRayStartPoint;

    if(Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
    {
      moveDirection = Vector3.zero;
    }

    if(playerManager.isInAir)
    {
      rigidbody.AddForce((-Vector3.up * fallingSpeed) + (moveDirection * fallingSpeed / 10f));
    }

    Vector3 dir = moveDirection;
    dir.Normalize();
    origin += dir * groundDirectionRayDistance;
    targetPosition = myTransform.position;
    Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
    if(Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
    {
      normalVector = hit.normal;
      Vector3 tp = hit.point;
      playerManager.isGrounded = true;
      targetPosition.y = tp.y;
      if(playerManager.isInAir)
      {
        if(inAirTimer > 0.5f)
        {
          Debug.Log("You were in the air for " + inAirTimer);
          animationHandler.PlayTargetAnimation("Land", true);
          inAirTimer = 0;
        }
        else
        {
          animationHandler.PlayTargetAnimation("Empty", false);
          inAirTimer = 0;
        }
        playerManager.isInAir = false;
      }
    }
    else
    {
      if(playerManager.isGrounded)
      {
        playerManager.isGrounded = false;
      }

      if(!playerManager.isInAir)
      {
        if(!playerManager.isInteracting)
        {
          animationHandler.PlayTargetAnimation("Falling", true);
        }

        Vector3 vel = rigidbody.velocity;
        vel.Normalize();
        rigidbody.velocity = vel * (movementSpeed / 2);
        playerManager.isInAir = true;
      } 
    }

    if(playerManager.isGrounded)
    {
      if(playerManager.isInteracting || inputHandler.moveAmount > 0)
      {
        myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.11f);
      }
      else
      {
        myTransform.position = targetPosition;
      }
    }
  }

  #endregion
}
