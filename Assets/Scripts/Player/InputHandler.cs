using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler: MonoBehaviour
{
  public float horizontal;
  public float vertical;
  public float moveAmount;
  public float mouseX;
  public float mouseY;

  public bool b_Input;

  public bool dodgeFlag;
  public bool sprintFlag;
  public float dodgeInputTimer;

  PlayerControls inputActions;

  Vector2 movementInput;
  Vector2 cameraInput;
  

  public void OnEnable()
  {
    if(inputActions == null)
    {
      inputActions = new PlayerControls();
      inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
      inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
    }
    inputActions.Enable();
  }

  private void OnDisable()
  {
    inputActions.Disable();
  }

  public void TickInput(float delta)
  {
    MoveInput(delta);
    HandleDodgeInput(delta);
  }

  private void MoveInput(float delta)
  {
    horizontal = movementInput.x;
    vertical = movementInput.y;

    moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    mouseX = cameraInput.x;
    mouseY = cameraInput.y;
  }

  private void HandleDodgeInput(float delta)
  {
    b_Input = inputActions.PlayerActions.Dodge.IsPressed();

    if (b_Input)
    {
      dodgeInputTimer += delta;
      if(dodgeInputTimer > 0.5f)
        sprintFlag = true;
    }
    else
    {
      if (dodgeInputTimer > 0 && dodgeInputTimer < 0.5f)
      {
        sprintFlag = false;
        dodgeFlag = true;
      }

      dodgeInputTimer = 0;
    }
  }
}
