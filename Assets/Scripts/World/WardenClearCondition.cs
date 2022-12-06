using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WardenClearCondition : BossManager
{
  public EnemyStats warden;
  public GameObject fogWall;
  public GameObject fogWall2;
  public GameObject healthbar;

  public TextMeshProUGUI bossText;
  
  public string bossName;

  void Start()
  {
    bossText.text = bossName;
  }

  void Update()
  {
    if(startFlag)
    {
      Debug.Log("Startflag is true");
      healthbar.SetActive(true);
      startFlag = false;
    }

    if(warden.isDead)
    {
      EndBossFight();
    }
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

  void EndBossFight()
  {
    fogWall.SetActive(false);
    fogWall2.SetActive(false);
    healthbar.SetActive(false);
    Victory();
  }
}
