using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsHandler : MonoBehaviour
{
  public PlayerStats playerStats;
  public GameObject fallingSound;
  //BoxCollider fallCollider;

  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      fallingSound.SetActive(true);
      
      playerStats.TakeDamage(9999);
      Destroy(gameObject);
    }
  }
/*
  void OnCollisionEnter(Collision other)
  {
    if(other.gameObject.tag == "Player")
    {
      Debug.Log("Collision successful");
    }
  }
*/
}
