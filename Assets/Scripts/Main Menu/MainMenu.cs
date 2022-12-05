using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstLevel;

    public void PlayGame() {
        SceneManager.LoadScene(firstLevel);
    }


    public void QuitGame() {
        Application.Quit();
    }


    public void GotoMain() {
      Debug.Log("Scene Loaded");
      SceneManager.LoadScene("Main Menu");
    }
}
