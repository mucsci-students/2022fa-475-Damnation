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
    }

     private int SetMaxHealthFromHealthLevel() {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
