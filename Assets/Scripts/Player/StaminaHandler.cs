using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaHandler : MonoBehaviour
{
  PlayerManager playerManager;
  PlayerStats playerStats;
  
  void Start()
  {
    playerManager = GetComponent<PlayerManager>();
    playerStats = GetComponent<PlayerStats>();
  }

  void Update()
  {
    if(!playerManager.isInteracting && !playerManager.isSprinting)
    {
      if(playerStats.currentStamina < playerStats.maxStamina)
      {
        playerStats.TakeStaminaDamage(-0.001f);
      }
    }
    else if(playerManager.isSprinting)
    {
      if(playerStats.currentStamina <= 0)
        playerStats.currentStamina = 0;
      else
        playerStats.TakeStaminaDamage(0.001f);
    }
  }
}
