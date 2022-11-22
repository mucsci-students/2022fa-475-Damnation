using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YB {
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        QuickSlotsUI quickSlotsUI;

        private void Awake() 
        {
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();


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

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft) {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
            }
        }
    }
}

