using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
  

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
            return maxHealth;
        }
    public void TakeDamage(int damage){

        currentHealth = currentHealth - damage;
        //animator.Play("Damage");
        if(currentHealth <= 0){

            currentHealth = 0;
            animator.Play("Death");
            isDead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
