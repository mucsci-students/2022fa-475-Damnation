using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeCollider : MonoBehaviour
{
    SphereCollider damageCollider;
    public int damage = 50;
    public EnemyStats enemyStats;

    private void Awake(){
        damageCollider = GetComponent<SphereCollider>();
        damageCollider.gameObject.SetActive(true);
        
    }

    public void EnableDamageCollider(){
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider(){

        damageCollider.enabled = false;
    }

   private void OnTriggerEnter(Collider other){

    if (enemyStats.isDead)
      return;

    if(other.tag == "Player"){
      
      PlayerStats playerStats = other.GetComponent<PlayerStats>();
        
        if(playerStats != null){
            playerStats.TakeDamage(damage);
        }
    }
        
        
    }
}
