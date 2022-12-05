using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkeletonClearCondition : BossManager
{
  public EnemyStats skeleton_1;
  public EnemyStats skeleton_2;
  public GameObject fogWall;
  public GameObject healthbars;
  public GameObject portalController;
  public bool skeleton_1_dead = false;
  public bool skeleton_2_dead = false;
  
  public TextMeshProUGUI bossText;
  public TextMeshProUGUI bossText2;
  
  public string bossName;

  void Start()
  {
    bossText.text = bossName;
    bossText2.text = bossName;
  }

  void Update()
  {
    if(startFlag)
    {
      healthbars.SetActive(true);
      startFlag = false;
    }
    
    if(skeleton_1.isDead)
    {
      skeleton_1_dead = true;
    }

    if(skeleton_2.isDead)
    {
      skeleton_2_dead = true;
    }

    if(bossFightActive && skeleton_1_dead && skeleton_2_dead)
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
    healthbars.SetActive(false);
    portalController.SetActive(true);
    Victory();
  }
}
