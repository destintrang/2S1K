using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public bool godMode;

    public GameObject winScreen;


    //Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }


    public void WinGame()
    {

        winScreen.SetActive(true);

    }


    public void LoseGame ()
    {

        if (godMode) {
            Debug.Log("DEATH");
            return; 
        }
        SceneManager.LoadSceneAsync(0);

    }

}
