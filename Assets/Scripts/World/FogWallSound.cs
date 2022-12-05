using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWallSound : MonoBehaviour
{
  public GameObject fogWallSound;

  void OnTriggerEnter(Collider other)
  {
    fogWallSound.SetActive(true);
    Destroy(gameObject);
  }
}
