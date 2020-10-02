using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{


    /// <summary>
    /// KEEPS TRACK OF THE PLAYER'S PROGRESS OVER ALL RUNS
    /// LOAD WHEN THE PLAYER TURNS ON THE GAME
    /// IS NEVER DESTROYED
    /// 
    /// SaveManager calls should just be done in these methods
    /// whenever these variables are modified
    /// </summary>


    //Change these to match the player data
    private int highScore = 0;
    
    private bool firstLevelCleared = false;
    private bool secondLevelCleared = false;
    private bool thirdLevelCleared = false;
    
    private float firstLevelClearTime = 0;
    private float secondLevelClearTime = 0;
    private float thirdLevelClearTime = 0;

    public int GetHighScore () { return highScore; }
    public bool GetFirstCleared () { return firstLevelCleared; }
    public bool GetSecondCleared () { return secondLevelCleared; }
    public bool GetThirdCleared () { return thirdLevelCleared; }
    public float GetFirstClearTime() { return firstLevelClearTime; }
    public float GetSecondClearTime() { return secondLevelClearTime; }
    public float GetThirdClearTime() { return thirdLevelClearTime; }



    public static StatsManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ImportPlayerData(SaveManager.LoadData());
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateHighScore (int score)
    {

        highScore = Mathf.Max(score, highScore);
        //Save after updating high score
        SaveManager.SaveData();

    }

    public void ClearLevel (int level, float levelTime)
    {
        
        if (level == 1)
        {
            firstLevelCleared = true;
            firstLevelClearTime = Mathf.Min(firstLevelClearTime, levelTime);
        }
        else if (level == 2)
        {
            secondLevelCleared = true;
            secondLevelClearTime = Mathf.Min(secondLevelClearTime, levelTime);
        }
        else if (level == 3)
        {
            thirdLevelCleared = true;
            thirdLevelClearTime = Mathf.Min(thirdLevelClearTime, levelTime);
        }

        //Save after
        SaveManager.SaveData();

    }



    //Load stats saved on the computer
    public void ImportPlayerData(PlayerData data)
    {

        if (data == null) { return; }

        highScore = data.highScore;

        firstLevelCleared = data.firstLevelCleared;
        secondLevelCleared = data.secondLevelCleared;
        thirdLevelCleared = data.thirdLevelCleared;

        firstLevelClearTime = data.firstLevelClearTime;
        secondLevelClearTime = data.secondLevelClearTime;
        thirdLevelClearTime = data.thirdLevelClearTime;

    }
    //Create and return a PlayerData object initialized with this object
    public PlayerData ExportPlayerData()
    {
        return new PlayerData(this);
    }

}