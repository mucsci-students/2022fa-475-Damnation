using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsHandler : MonoBehaviour
{
  PlayerStats playerStats;
  public GameObject fallingSound;
  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      fallingSound.SetActive(true);
      playerStats = other.gameObject.GetComponent<PlayerStats>();
      playerStats.TakeDamage(9999);
    }
  }
}
