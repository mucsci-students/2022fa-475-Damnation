using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
  AnimationHandler animationHandler;
  PlayerStats playerStats;
  PlayerManager playerManager;
  StaminaHandler staminaHandler;

  private void Awake()
  {
    animationHandler = GetComponentInChildren<AnimationHandler>();
    playerStats = GetComponent<PlayerStats>();
    playerManager = GetComponent<PlayerManager>();
    staminaHandler = GetComponent<StaminaHandler>();
  }

  public void HandleLightAttack(WeaponItem weapon)
  {
    if(playerStats.isDead || playerManager.outOfStamina || playerStats.currentStamina < staminaHandler.lightAttackCost)
      return;
    
    playerStats.TakeStaminaDamage(staminaHandler.lightAttackCost);
    animationHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
  }

  public void HandleHeavyAttack(WeaponItem weapon)
  {
    if(playerStats.isDead || playerManager.outOfStamina || playerStats.currentStamina < staminaHandler.heavyAttackCost)
      return;
    playerStats.TakeStaminaDamage(staminaHandler.heavyAttackCost);
    animationHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
  }
}
