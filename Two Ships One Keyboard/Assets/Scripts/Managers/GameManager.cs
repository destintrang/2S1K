using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    //Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }


    public void LoseGame ()
    {

        SceneManager.LoadSceneAsync(0);

    }

}
