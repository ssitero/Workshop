using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{

    void Start()
    {

    }

    public void PlayGame (){
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayBattle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }


    void Update()
    {
        if (Input.GetKey("escape")){
          Application.Quit();
        }
    }
}
