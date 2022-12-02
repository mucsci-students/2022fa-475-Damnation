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
            animationHandler = GetComponentInChildren<AnimationHandler>();
        }

        private int SetMaxHealthFromHealthLevel() {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if(isDead){
                return;
            }
            currentHealth = currentHealth - damage;
            healthbar.SetCurrentHealth(currentHealth);
            animationHandler.PlayTargetAnimation("knight_001_face_protection", true);


            if(currentHealth <= 0){

                currentHealth = 0;
                animationHandler.PlayTargetAnimation("knight_001_death1", true);
                isDead = true;
                //You Died screen

            }
        }
    }


