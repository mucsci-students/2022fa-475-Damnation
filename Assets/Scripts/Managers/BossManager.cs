using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
  public bool bossFightActive = false;
  public GameObject bossMusic;
  public GameObject levelMusic;
  public GameObject victorySound;
  public GameObject victoryScreen;
  
  [HideInInspector]
  public bool startFlag = false;
  public bool victoryFlag = false;
  
  [HideInInspector]
  public float victoryScreenTimer = 0f;
  public float maxTimer = 8f; 

  public void Victory()
  {
    bossFightActive = false;
    bossMusic.SetActive(false);
    levelMusic.SetActive(true);
    victorySound.SetActive(true);
    victoryFlag = true;
  }

  public void InitiateBossFight()
  {
    bossFightActive = true;
    startFlag = true;
    levelMusic.SetActive(false);
    bossMusic.SetActive(true);
  }
}
