using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChurchClearCondition : BossManager
{
  public GameObject[] enemies;
  public GameObject fogWall;
  public GameObject enemyContainer;
  public GameObject objective;
  public GameObject portalController;

  int deadEnemies = 0;
  public TextMeshProUGUI bossText;
  string killCount;

  void Update()
  {
    killCount = deadEnemies.ToString() + "/" + enemies.Length.ToString();
    bossText.text = killCount;
    deadEnemies = 0;
    if(startFlag)
    {
      SpawnEnemies();
      objective.SetActive(true);
    }
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
      if(enemyStats.isDead)
        deadEnemies++;
    }
    if(deadEnemies == enemies.Length)
      EndBossFight();
  }

  void EndBossFight()
  {
    fogWall.SetActive(false);
    objective.SetActive(false);
    portalController.SetActive(true);
    Victory();
  }
}
