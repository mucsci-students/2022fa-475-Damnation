using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
  public GameObject fogWall;
  public GameObject lockedFogWall;
  public GameObject unlockSound;

  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      lockedFogWall.SetActive(false);
      fogWall.SetActive(true);
      unlockSound.SetActive(true);
      Destroy(gameObject);
    }
  }
}
