﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles both points and coins
public class ScoreManager : MonoBehaviour
{


    private int score;
    public int GetScore () { return score; }


    [SerializeField] protected int coins;
    private int coinsNeeded = 20;

    private int upgradeCount = 0;
    delegate void UpgradeFunction();
    private List<UpgradeFunction> redUpgrades = new List<UpgradeFunction> { };
    private List<UpgradeFunction> blueUpgrades = new List<UpgradeFunction> { };


    // Start is called before the first frame update
    void Start()
    {

        //Initialize score/coins
        score = 0;
        coins = 0;

        //Initialize upgrades
        InitializeUpgrades();


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



    //UPGRADE STUFF
    private void LevelUp ()
    {

        coins = 0;
        coinsNeeded += 5;

        Debug.Log("UPGRADE");
        UpgradeShips();

    }

    private void UpgradeShips ()
    {

        int randomIndex = Random.Range(0, redUpgrades.Count);
        redUpgrades[randomIndex]();
        redUpgrades.RemoveAt(randomIndex);

        randomIndex = Random.Range(0, blueUpgrades.Count);
        blueUpgrades[randomIndex]();
        blueUpgrades.RemoveAt(randomIndex);

    }

    //Called at the start of the game
    private void InitializeUpgrades ()
    {

        redUpgrades.Add(UpgradeRedGuns);
        redUpgrades.Add(UpgradeRedAttackSpeed);
        redUpgrades.Add(UpgradeRedProjectileSpeed);
        redUpgrades.Add(UpgradeRedMovementSpeed);

        blueUpgrades.Add(UpgradeBlueShieldSize);
        blueUpgrades.Add(UpgradeBlueMovementSpeed);
        blueUpgrades.Add(UpgradeBlueDischarge);
        blueUpgrades.Add(UpgradeBlueCooldown);

    }

    //Red upgrades
    void UpgradeRedDamage()
    {

    }
    void UpgradeRedGuns()
    {
        FindObjectOfType<AttackShip>().UpgradeGuns();
    }
    void UpgradeRedAttackSpeed()
    {
        FindObjectOfType<AttackShip>().UpgradeAttackSpeed();
    }
    void UpgradeRedProjectileSpeed()
    {
        FindObjectOfType<AttackShip>().UpgradeProjectileSpeed();
    }
    void UpgradeRedMovementSpeed()
    {
        FindObjectOfType<AttackShip>().UpgradeMovementSpeed();
    }
    //Blue upgrades
    private void UpgradeBlueShieldSize()
    {
        FindObjectOfType<DefenseShip>(). UpgradeShieldSize();
    }
    private void UpgradeBlueMovementSpeed()
    {
        FindObjectOfType<DefenseShip>().UpgradeMovementSpeed();
    }
    private void UpgradeBlueCooldown()
    {
        FindObjectOfType<DefenseShip>().UpgradeDischargeCooldown();
    }
    private void UpgradeBlueDischarge()
    {
        FindObjectOfType<DefenseShip>().UpgradeDischargeSize();
    }

}
