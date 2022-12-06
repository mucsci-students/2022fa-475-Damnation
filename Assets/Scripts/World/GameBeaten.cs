using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBeaten : MonoBehaviour
{
  public GameObject prompt;

  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      prompt.SetActive(true);
    }
  }
}
