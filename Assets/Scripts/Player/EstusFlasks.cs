using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EstusFlasks : MonoBehaviour
{
  public int flaskCount = 3;
  public int healAmount = 50;
  public PlayerStats playerStats;
  public TextMeshProUGUI estusText;

  void Update()
  {
    estusText.text = flaskCount.ToString();
  }

  public void DrinkFlask()
  {
    playerStats.TakeDamage(-healAmount);
  }
}
