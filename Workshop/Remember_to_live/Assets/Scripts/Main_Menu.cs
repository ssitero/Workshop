using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Main_Menu : MonoBehaviour
{
    public string mainMenu;
    public string artScene;
    public string techScene;

    void Start()
    {

    }

    public void PlayArt (){
      SceneManager.LoadScene(artScene);
    }

    public void PlayBattle()
    {
        SceneManager.LoadScene(techScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKey("escape")){
          Application.Quit();
        }
    }
}
