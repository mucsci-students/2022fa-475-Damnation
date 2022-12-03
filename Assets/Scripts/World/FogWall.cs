using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
  public GameObject fogWallCollider;
  public GameObject levelMusicObj;
  public GameObject bossMusicObj;

  void OnTriggerEnter(Collider other)
  {
    fogWallCollider.SetActive(true);
    levelMusicObj.SetActive(false);
    bossMusicObj.SetActive(true);
    Destroy(gameObject);
  }
}
