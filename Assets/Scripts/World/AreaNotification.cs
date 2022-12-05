using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaNotification : MonoBehaviour
{
  public TextMeshProUGUI areaName;
  public string message;
  private float timer = 0f;
  public float maxTimer = 2f;

  void Start()
  {
    areaName.text = message;
  }

  void Update()
  {
    timer += Time.deltaTime;

    if(timer >= maxTimer)
      fadeOut();
  }

  void fadeOut()
  {
    Destroy(gameObject);
    areaName.CrossFadeAlpha(0.0f, 1.5f, false);
  }
}
