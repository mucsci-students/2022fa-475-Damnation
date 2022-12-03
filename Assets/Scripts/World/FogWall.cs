using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
  public GameObject fogWallCollider;
  public BossManager bossManager;

  void OnTriggerEnter(Collider other)
  {
    fogWallCollider.SetActive(true);
    bossManager.InitiateBossFight();
    Destroy(gameObject);
  }
}
