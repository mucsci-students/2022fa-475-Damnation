using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
  WeaponHolderSlot leftHandSlot;
  WeaponHolderSlot rightHandSlot;

  DamageCollider leftHandDamageCollider;
  DamageCollider rightHandDamageCollider;

  Animator animator;

  PlayerStats playerStats;
  InputHandler inputHandler;

  PlayerManager playerManager;

  //QuickSlotUI quickSlotsUI;

  private void Awake() 
  {
    //quickSlotsUI = FindObjectOfType<QuickSlotUI>();
    playerStats = GetComponentInParent<PlayerStats>();
    inputHandler = GetComponentInParent<InputHandler>();
    playerManager = GetComponentInParent<PlayerManager>();
    animator = GetComponent<Animator>();

    WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
    foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots) 
    {
      if (weaponSlot.isLeftHandSlot) 
      {
        leftHandSlot = weaponSlot;
        
      } 
      else if (weaponSlot.isRightHandSlot) 
      {
        rightHandSlot = weaponSlot;
      }
    }
  }

  public void LoadLeftWeaponDamageCollider(){

    leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }
  public void LoadRightWeaponDamageCollider(){
    rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
  }

  public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft) 
  {
    if (isLeft)
    {
      leftHandSlot.LoadWeaponModel(weaponItem);
      //quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
      LoadLeftWeaponDamageCollider();
    }
    else
    {
      rightHandSlot.LoadWeaponModel(weaponItem);
      //quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
      LoadRightWeaponDamageCollider();
    }
  }
  
  #region handle damage colliders
  
public void OpenDamageCollider(){
    if (playerManager.isUsingRightHand){
      Debug.Log("tacos");
      rightHandDamageCollider.EnableDamageCollider();
    }
    else if (playerManager.isUsingLeftHand){

      leftHandDamageCollider.EnableDamageCollider();
    }

  }

  public void CloseDamageCollider(){

    rightHandDamageCollider.DisableDamageCollider();
    leftHandDamageCollider.DisableDamageCollider();
  }
  #endregion




}



