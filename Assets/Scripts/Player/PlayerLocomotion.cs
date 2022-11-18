using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
  Transform cameraObject;
  InputHandler inputHandler;
  Vector3 moveDirection;
  //[SerializeField] Transform playerRig;
  ///[SerializeField] Transform rigTransform;

  [HideInInspector]
  public Transform myTransform;
  //[HideInInspector]
  public AnimationHandler animationHandler;

  public new Rigidbody rigidbody;
  public GameObject normalCamera;

  [Header("Stats")]
  [SerializeField]
  float movementSpeed = 5;
  [SerializeField]
  float rotationSpeed = 10;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    inputHandler = GetComponent<InputHandler>();
    //animationHandler = GetComponent<AnimationHandler>();
    cameraObject = Camera.main.transform;
    myTransform = transform;
    animationHandler.Initialize();
  }


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
  #endregion
}
