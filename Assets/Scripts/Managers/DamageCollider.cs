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
        //damageCollider.isTrigger = true;
        damageCollider.enabled = false;

    }

    public void EnableDamageCollider(){
        Debug.Log("enable sword");
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider(){

        damageCollider.enabled = false;
    }

    void OnCollisionEnter(Collider collision){

        Debug.Log("entered");
        print("hello");
        if(collision.tag == "Player"){

            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        
        if(playerStats != null){

            playerStats.TakeDamage(weaponDamage);
        }
        }
        if(collision.tag == "enemy"){
            Debug.Log("enemy collider");
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            
        
        if(enemyStats != null){
            
            enemyStats.TakeDamage(weaponDamage);
        }
        }
    }
}
