using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInventory : MonoBehaviour
{
  WeaponSlotManager weaponSlotManager;

  public WeaponItem rightWeapon;
  public WeaponItem leftWeapon;

  //public WeaponItem unarmedWeapon;

  //public WeaponItem[] weaponInRightHandSlots = new WeaponItem[1];
  
  //public WeaponItem[] weaponInLeftHandSlots = new WeaponItem[1];

  //public int currentRightWeaponIndex = 0;
  //public int currentLeftWeaponIndex = 0;

  private void Awake() 
  {
    weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
  }

  private void Start() 
  {
    //rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
    //leftWeapon = weaponInRightHandSlots[currentLeftWeaponIndex];
    weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
    weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
  }
/*
  public void ChangeRightWeapon() 
  {
    currentRightWeaponIndex = currentRightWeaponIndex + 1;
    if (currentRightWeaponIndex == 0 && weaponInRightHandSlots[0] != null)
    {
      rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
    }
    else if (currentRightWeaponIndex == 0 && weaponInRightHandSlots[0] == null)
    {
      currentRightWeaponIndex = currentRightWeaponIndex + 1;
    }
    else if (currentRightWeaponIndex == 1 && weaponInRightHandSlots[1] != null)
    {
      rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
    }
    else 
    {
      currentRightWeaponIndex = currentRightWeaponIndex + 1;
    }

    if (currentRightWeaponIndex > weaponInRightHandSlots.Length - 1)
    {
      currentRightWeaponIndex = -1;
      rightWeapon = unarmedWeapon;
      weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
    }
  }*/
}

