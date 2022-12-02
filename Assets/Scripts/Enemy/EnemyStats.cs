using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
  
    public HealthBar healthbar;
    Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start(){

        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        isDead = false;
    }

     private int SetMaxHealthFromHealthLevel() {
            maxHealth = healthLevel * 10;
            healthbar.SetMaxHealth(maxHealth);
            return maxHealth;
        }
    public void TakeDamage(int damage){
        if(isDead){
            return;
        }
        currentHealth = currentHealth - damage;
        healthbar.SetCurrentHealth(currentHealth);
        animator.Play("Damage");
        if(currentHealth <= 0){

            currentHealth = 0;
            animator.Play("Death");
            animator.SetBool("isDead", true);
            isDead = true;

        }
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
