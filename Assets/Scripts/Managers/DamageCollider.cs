using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    BoxCollider damageCollider;
    public int weaponDamage = 25;

    private void Awake(){
        damageCollider = GetComponent<BoxCollider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;

    }

    public void EnableDamageCollider(){
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider(){

        damageCollider.enabled = false;
    }

   private void OnTriggerEnter(Collider other){


        if(other.tag == "Player"){

            PlayerStats playerStats = other.GetComponent<PlayerStats>();
        
        if(playerStats != null){

            playerStats.TakeDamage(weaponDamage);
        }
        }
        if(other.tag == "enemy"){
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            
        
        if(enemyStats != null){
            
            enemyStats.TakeDamage(weaponDamage);
        }
        }
    }
}
