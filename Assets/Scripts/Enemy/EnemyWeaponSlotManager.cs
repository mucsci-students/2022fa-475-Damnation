using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSlotManager : MonoBehaviour
{
    public WeaponItem rightHandWeapon;
    public WeaponItem leftHandWeapon;


   WeaponHolderSlot rightHandSlot;
   WeaponHolderSlot leftHandSlot;

   DamageCollider leftHandDamageCollider;
   DamageCollider rightHandDamageCollider;

   private void Start(){
    LoadWeaponsOnBothHands();
   }

   public void LoadWeaponOnSlot(WeaponItem weapon, bool isLeft){

    if(isLeft){

       // leftHandSlot.currentWeapon = weapon;
        leftHandSlot.LoadWeaponModel(weapon);
    }
    else{
       // rightHandSlot.currentWeapon = weapon;
        rightHandSlot.LoadWeaponModel(weapon);

    }
   }

   public void LoadWeaponsOnBothHands(){

    if(rightHandWeapon != null){

        LoadWeaponOnSlot(rightHandWeapon, false);
    }
    if(leftHandWeapon != null){
        LoadWeaponOnSlot(leftHandWeapon, true);
    }
   }

   public void LoadWeaponsDamageCollider(bool isLeft){

    if(isLeft){
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();


  }
  else{
    rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

  }

   }
}
