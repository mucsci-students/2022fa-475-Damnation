using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
  public PlayerStats player;
  public GameObject deathScreen;
  public GameObject deathSound;
  public GameObject levelMusic;
  public GameObject bossMusic;

  public string sceneName;
  public bool activated = false;
  float deathScreenTimer = 0f;
  public float maxTimer = 6f;

  void Update()
  {
    if(!activated && player.isDead)
    {
      deathScreen.SetActive(true);
      deathSound.SetActive(true);
      levelMusic.SetActive(false);
      bossMusic.SetActive(false);
      activated = true;
    }

    if(activated)
    {
      deathScreenTimer += Time.deltaTime;
      if(deathScreenTimer >= maxTimer)
        SceneManager.LoadScene(sceneName);
    }
  }
}
