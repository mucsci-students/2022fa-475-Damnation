using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : CharacterStats
{
    public HealthBar healthbar;
    StaminaBar staminabar;
    AnimationHandler animationHandler;

    private void Awake() {
        healthbar = FindObjectOfType<HealthBar>();
        staminabar = FindObjectOfType<StaminaBar>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }

        void Start() {
            isDead = false;
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
        }
    
    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

        public void TakeDamage(int damage)
        {   
             if(isDead){
                return;
             }
            currentHealth = currentHealth - damage;
            animationHandler.PlayTargetAnimation("knight_001_face_protection", true);
            healthbar.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0)
        {
                
                currentHealth = 0;
                animationHandler.PlayTargetAnimation("knight_001_death1", true);
                isDead = true;
                animationHandler.isDead(isDead);
                

        }
    }

    public void TakeStaminaDamage(int damage)
    {
        currentStamina = currentStamina - damage;

        staminabar.SetCurrentStamina(currentStamina);
    }
}


