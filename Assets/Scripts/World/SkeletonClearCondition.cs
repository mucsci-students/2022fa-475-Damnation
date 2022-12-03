using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonClearCondition : BossManager
{
  public EnemyStats skeleton_1;
  public EnemyStats skeleton_2;
  public GameObject fogWall;
  public GameObject healthbars;
  public GameObject portalController;
  
  void Update()
  {
    if(startFlag)
    {
      healthbars.SetActive(true);
      startFlag = false;
    }

    if(bossFightActive && skeleton_1.isDead && skeleton_2.isDead)
    {
      EndBossFight();
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
