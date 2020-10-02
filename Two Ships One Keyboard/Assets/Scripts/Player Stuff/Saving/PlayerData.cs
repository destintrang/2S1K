using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{


    /// <summary>
    /// Customize this to hold whatever info needs to be saved after quitting, based on the game
    /// This doesn't ever have to be put into a scene
    /// </summary>


    public int highScore;

    public bool firstLevelCleared;
    public bool secondLevelCleared;
    public bool thirdLevelCleared;

    public float firstLevelClearTime;
    public float secondLevelClearTime;
    public float thirdLevelClearTime;


    public PlayerData()
    {

        highScore = 0;

        firstLevelCleared = false;
        secondLevelCleared = false;
        thirdLevelCleared = false;

        firstLevelClearTime = 0;
        secondLevelClearTime = 0;
        thirdLevelClearTime = 0;

    }
    public PlayerData(StatsManager s)
    {

        highScore = s.GetHighScore();

        firstLevelCleared = s.GetFirstCleared();
        secondLevelCleared = s.GetSecondCleared();
        thirdLevelCleared = s.GetThirdCleared();

        firstLevelClearTime = s.GetFirstClearTime();
        secondLevelClearTime = s.GetSecondClearTime();
        thirdLevelClearTime = s.GetThirdClearTime();

    }




}