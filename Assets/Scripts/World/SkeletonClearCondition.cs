using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonClearCondition : BossManager
{
  public EnemyStats skeleton_1;
  public EnemyStats skeleton_2;
  public GameObject fogWall;
  
  void Update()
  {
    if(startFlag)
    {
      // Activate boss health bars;
      Debug.Log("Healthbars Activated");
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
    //Activate portal here
    Victory();
  }
}
