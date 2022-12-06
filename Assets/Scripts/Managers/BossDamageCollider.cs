using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageCollider : MonoBehaviour
{
  BoxCollider damageCollider;
  
  public int weaponDamage = 25;

  private void Awake()
  {
    damageCollider = GetComponent<BoxCollider>();
    damageCollider.gameObject.SetActive(true);
    damageCollider.isTrigger = true;
    //damageCollider.enabled = false;
    EnableDamageCollider();
  }

  public void EnableDamageCollider()
  {
    Debug.Log("BOSS SWORD ENABLED");
    damageCollider.enabled = true;
  }

  public void DisableDamageCollider()
  {
    damageCollider.enabled = false;
  }

  private void OnTriggerEnter(Collider other)
  {
    
    if(other.tag == "Player")
    {
      Debug.Log("Player in hitbox");
      PlayerStats playerStats = other.GetComponent<PlayerStats>();
        
      if(playerStats != null)
      {
          playerStats.TakeDamage(weaponDamage);
      }
    }    
  }
}
