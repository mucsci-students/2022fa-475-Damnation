using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
  public GameObject messageBox;
  public TextMeshProUGUI messageText;
  public string message;

  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      messageBox.SetActive(true);
      messageText.text = message;
    }
  }

  void OnTriggerExit(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      messageBox.SetActive(false);
    }
  }
}
