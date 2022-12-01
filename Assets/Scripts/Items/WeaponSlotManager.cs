using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
  WeaponHolderSlot leftHandSlot;
  WeaponHolderSlot rightHandSlot;

  DamageCollider leftHandDamageCollider;
  DamageCollider rightHandDamageCollider;

  //QuickSlotUI quickSlotsUI;

  private void Awake() 
  {
    //quickSlotsUI = FindObjectOfType<QuickSlotUI>();


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
  public void OpenRightDamageCollider(){

    rightHandDamageCollider.EnableDamageCollider();
  }

  public void OpenLeftDamageCollider(){

    leftHandDamageCollider.EnableDamageCollider();
  }

  public void CloseRightDamageCollider(){

    rightHandDamageCollider.DisableDamageCollider();
  }

  public void CloseLeftDamageCollider(){

    leftHandDamageCollider.DisableDamageCollider();
  }
  #endregion
}


