using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
  public bool bossFightActive = false;
  public GameObject bossMusic;
  public GameObject levelMusic;
  public GameObject VictorySound;
  [HideInInspector]
  public bool startFlag = false;

  public void Victory()
  {
    bossFightActive = false;
    bossMusic.SetActive(false);
    levelMusic.SetActive(true);
    VictorySound.SetActive(true);
    
    // Activate victory screen here
  }

  public void InitiateBossFight()
  {
    bossFightActive = true;
    startFlag = true;
    levelMusic.SetActive(false);
    bossMusic.SetActive(true);
  }
}
