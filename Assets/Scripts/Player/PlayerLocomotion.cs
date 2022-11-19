using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
  PlayerManager playerManager;
  Transform cameraObject;
  InputHandler inputHandler;
  Vector3 moveDirection;


  [HideInInspector]
  public Transform myTransform;
  //[HideInInspector]
  public AnimationHandler animationHandler;

  public new Rigidbody rigidbody;
  public GameObject normalCamera;

  [Header("Movement Stats")]
  [SerializeField]
  float movementSpeed = 5;
  [SerializeField]
  float sprintSpeed = 7;
  [SerializeField]
  float rotationSpeed = 10;

  void Start()
  {
    playerManager = GetComponent<PlayerManager>();
    rigidbody = GetComponent<Rigidbody>();
    inputHandler = GetComponent<InputHandler>();
    //animationHandler = GetComponent<AnimationHandler>();
    cameraObject = Camera.main.transform;
    myTransform = transform;
    animationHandler.Initialize();
  }
<<<<<<< Updated upstream


  public void Update()
  {
    //playerRig.position = rigTransform.position;
    

    float delta = Time.deltaTime;
    inputHandler.TickInput(delta);
        
    moveDirection = cameraObject.forward * inputHandler.vertical;
    moveDirection += cameraObject.right * inputHandler.horizontal;
    moveDirection.Normalize();
    moveDirection.y = 0;
    
    float speed = movementSpeed;
    moveDirection *= speed;

    Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
    rigidbody.velocity = projectedVelocity;
    //rigidbody.AddForce(Physics.gravity * 1f, ForceMode.Acceleration);
    
    
    animationHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);
    
    if(animationHandler.canRotate)
    {
      handleRotation(delta);
      //playerRig.rotation = rigTransform.rotation;
    }
  }
=======
>>>>>>> Stashed changes
  
  #region Movement


  Vector3 normalVector;
  Vector3 targetPosition;

  private void handleRotation(float delta)
  {
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
    if(inputHandler.dodgeFlag)
      return;

    moveDirection = cameraObject.forward * inputHandler.vertical;
    moveDirection += cameraObject.right * inputHandler.horizontal;
    moveDirection.Normalize();
    moveDirection.y = 0;
    
    float speed = movementSpeed;

    if(inputHandler.sprintFlag)
    {
      speed = sprintSpeed;
      playerManager.isSprinting = true;
      moveDirection *= speed;
    }
    else
      moveDirection *= speed;

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
    if(animationHandler.anim.GetBool("isInteracting"))
      return;

    if(inputHandler.dodgeFlag)
    {
      moveDirection = cameraObject.forward * inputHandler.vertical;
      moveDirection += cameraObject.right * inputHandler.horizontal;

      if(inputHandler.moveAmount > 0)
      {
        animationHandler.PlayTargetAnimation("Dodge", true);
        moveDirection.y = 0;
        Quaternion dodgeRotation = Quaternion.LookRotation(moveDirection);
        myTransform.rotation = dodgeRotation;
      }
      else
      {
        animationHandler.PlayTargetAnimation("Dodge", true);
      }
    }
  }

  #endregion
}
