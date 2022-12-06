using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
  public PlayerStats playerStats;
  public GameObject fallSound;
  /*
  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      playerStats.TakeDamage(10);
      
      if(playerStats.isDead)
        fallSound.SetActive(true);
      
    }
  }
*/
  void OnTriggerStay(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      playerStats.TakeDamage(10);
      
      if(playerStats.isDead)
        fallSound.SetActive(true);
      
    }
  }
}
