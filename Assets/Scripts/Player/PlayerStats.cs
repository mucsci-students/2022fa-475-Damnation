using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : CharacterStats
{
  public HealthBar healthbar;
  public StaminaBar staminabar;
  PlayerManager playerManager;
  AnimationHandler animationHandler;

  private void Awake() 
  {
    animationHandler = GetComponentInChildren<AnimationHandler>();
  }

  void Start() 
  {
    isDead = false;
    maxHealth = SetMaxHealthFromHealthLevel();
    currentHealth = maxHealth;
    healthbar.SetMaxHealth(maxHealth);
    playerManager = GetComponent<PlayerManager>();
  }
    
  private int SetMaxHealthFromHealthLevel()
  {
    maxHealth = healthLevel * 10;
    return maxHealth;
  }

  public void TakeDamage(int damage)
  {   
    if(isDead || playerManager.invincibilityFlag)
      return;

    currentHealth = currentHealth - damage;
    animationHandler.PlayTargetAnimation("knight_001_face_protection", true);
    healthbar.SetCurrentHealth(currentHealth);

    if (currentHealth <= 0)
    {      
      currentHealth = 0;
      animationHandler.PlayTargetAnimation("Death", true);
      isDead = true;
    }
  }

  public void TakeStaminaDamage(int damage)
  {
    currentStamina = currentStamina - damage;
    staminabar.SetCurrentStamina(currentStamina);
  }
}


