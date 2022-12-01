using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerStats : CharacterStats
    {
        public HealthBar healthbar;
        AnimationHandler animationHandler;

        void Start() {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevel() {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            healthbar.SetCurrentHealth(currentHealth);

            if(currentHealth <= 0){

                currentHealth = 0;

            }
        }
    }


