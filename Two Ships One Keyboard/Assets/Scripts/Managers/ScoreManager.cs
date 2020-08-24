using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles both points and coins
public class ScoreManager : MonoBehaviour
{


    private int score;
    public int GetScore () { return score; }


    [SerializeField] protected int coins;
    private int coinsNeeded = 100;
    private int upgradeCount = 0;


    // Start is called before the first frame update
    void Start()
    {

        //Initialize score/coins
        score = 0;
        coins = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void IncreaseScore (int increase)
    {

        FindObjectOfType<HUDManager>().UpdateScore(score, score + increase);
        score += increase;

    }
    public void IncreaseCoin ()
    {

        coins++;
        //Players will also get some points for coins they collect
        IncreaseScore(100);

        if (coins >= coinsNeeded)
        {

            //Level up
            LevelUp();

        }
        FindObjectOfType<HUDManager>().UpdateCoin(coins, coinsNeeded);

    }


    private void LevelUp ()
    {

        coins = 0;
        coinsNeeded += 50;
        Debug.Log("UPGRADE");

    }


}
