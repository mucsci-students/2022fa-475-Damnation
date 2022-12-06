using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponSlotManager : MonoBehaviour
{
  public WeaponItem rightHandWeapon;
  public WeaponItem leftHandWeapon;


  WeaponHolderSlot rightHandSlot;
  WeaponHolderSlot leftHandSlot;

  BossDamageCollider leftHandDamageCollider;
  BossDamageCollider rightHandDamageCollider;
  EnemyManager enemyManager;
  
  public GameObject aoeCollider;

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

  private void Start()
  {
    LoadWeaponsOnBothHands();
    enemyManager = GetComponent<EnemyManager>();
  }

  public void LoadWeaponOnSlot(WeaponItem weapon, bool isLeft)
  {

    if(isLeft)
    {
      //leftHandSlot.currentWeapon = weapon;
      leftHandSlot.LoadWeaponModel(weapon);
      LoadWeaponsDamageCollider(true);
    }
    
    else
    {
      //rightHandSlot.currentWeapon = weapon;
      rightHandSlot.LoadWeaponModel(weapon);
      LoadWeaponsDamageCollider(false);
    }
  }

  public void LoadWeaponsOnBothHands()
  {

    if(rightHandWeapon != null)
    {
      LoadWeaponOnSlot(rightHandWeapon, false);
    }

    if(leftHandWeapon != null)
    {
      LoadWeaponOnSlot(leftHandWeapon, true);
    }
  }

  public void LoadWeaponsDamageCollider(bool isLeft)
  {
    if(isLeft)
    {
      leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<BossDamageCollider>();
    }
    else
    {
      rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<BossDamageCollider>();
    }
  }
 
  public void OpenDamageCollider()
  {
    rightHandDamageCollider.EnableDamageCollider();
  }

  public void CloseDamageCollider()
  {
    rightHandDamageCollider.DisableDamageCollider();
  }

  public void OpenAOECollider()
  {
    aoeCollider.SetActive(true);
  }

  public void CloseAOECollider()
  {
    aoeCollider.SetActive(false);
  }
}
