using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles both points and coins
public class ScoreManager : MonoBehaviour
{


    //POINT STUFF
    private int score;
    public int GetScore () { return score; }


    //COINS AND UPGRADE STUFF
    [SerializeField] protected int coins;
    private int coinsNeeded = 20;

    private int upgradeCount = 0;
    delegate string UpgradeFunction();
    private List<UpgradeFunction> redUpgrades = new List<UpgradeFunction> { };
    private List<UpgradeFunction> blueUpgrades = new List<UpgradeFunction> { };


    //TIMER STUFF
    float time = 0;
    //Toggle this when the game starts
    private bool timerStarted = false;
    public void StartTimer () { timerStarted = true; }


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
    void FixedUpdate()
    {
        if (!timerStarted) { return; }
        UpdateTime();
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
        string r = redUpgrades[randomIndex]();
        redUpgrades.RemoveAt(randomIndex);

        randomIndex = Random.Range(0, blueUpgrades.Count);
        string b = blueUpgrades[randomIndex]();
        blueUpgrades.RemoveAt(randomIndex);

        FindObjectOfType<HUDManager>().DisplayUpgradeText(r, b);

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
    string UpgradeRedDamage()
    {
        return ("Damage+");
    }
    string UpgradeRedGuns()
    {
        FindObjectOfType<AttackShip>().UpgradeGuns();
        return ("Guns+");
    }
    string UpgradeRedAttackSpeed()
    {
        FindObjectOfType<AttackShip>().UpgradeAttackSpeed();
        return ("Fire Rate+");
    }
    string UpgradeRedProjectileSpeed()
    {
        FindObjectOfType<AttackShip>().UpgradeProjectileSpeed();
        return ("Bullet Speed+");
    }
    string UpgradeRedMovementSpeed()
    {
        FindObjectOfType<AttackShip>().UpgradeMovementSpeed();
        return ("Speed+");
    }
    //Blue upgrades
    private string UpgradeBlueShieldSize()
    {
        FindObjectOfType<DefenseShip>(). UpgradeShieldSize();
        return ("Shield+");
    }
    private string UpgradeBlueMovementSpeed()
    {
        FindObjectOfType<DefenseShip>().UpgradeMovementSpeed();
        return ("Speed+");
    }
    private string UpgradeBlueCooldown()
    {
        FindObjectOfType<DefenseShip>().UpgradeDischargeCooldown();
        return ("Cooldown Reduced");
    }
    private string UpgradeBlueDischarge()
    {
        FindObjectOfType<DefenseShip>().UpgradeDischargeSize();
        return ("Shockwave Size+");
    }


    private void UpdateTime ()
    {
        time += Time.deltaTime;
        FindObjectOfType<HUDManager>().UpdateTime(time);
    }


}
