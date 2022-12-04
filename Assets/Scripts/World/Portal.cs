using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
  public string sceneName;
  
  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      SceneManager.LoadScene(sceneName);
    }
  }
}
