using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchClearCondition : BossManager
{
  public GameObject[] enemies;
  public GameObject fogWall;
  public GameObject enemyContainer;

  void Update()
  {
    if(startFlag)
      SpawnEnemies();

    if(bossFightActive && !startFlag)
      CheckEnemyHealth();

    if(victoryFlag)
    {
      victoryScreen.SetActive(true);
      victoryScreenTimer += Time.deltaTime;
      if(victoryScreenTimer >= maxTimer)
      {
        victoryScreen.SetActive(false);
        victoryFlag = false;
      }
    }
  }

  void SpawnEnemies()
  {
    enemyContainer.SetActive(true);
    startFlag = false;
  }

  void CheckEnemyHealth()
  {
    for(int i = 0; i < enemies.Length; i++)
    {
      EnemyStats enemyStats = enemies[i].GetComponent<EnemyStats>();
      if(!enemyStats.isDead)
        return;
    }
    
    EndBossFight();
  }

  void EndBossFight()
  {
    fogWall.SetActive(false);
    //Activate portal here
    Victory();
  }
}
