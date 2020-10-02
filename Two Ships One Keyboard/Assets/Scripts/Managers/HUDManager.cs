using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is for managing everything in the UI
public class HUDManager : MonoBehaviour
{


    [SerializeField] protected Text scoreText;
    [SerializeField] protected Text highScoreText;
    [SerializeField] protected float scoreUpdateTime;


    [SerializeField] protected Image coinBar;
    [SerializeField] protected float barUpdateTime;

    [SerializeField] protected Text redUpgrade;
    [SerializeField] protected Text blueUpgrade;
    [SerializeField] protected float upgradeTextDuration;
    private float flashDuration = 10;


    [SerializeField] protected Text timeText;


    [SerializeField] protected Image bossHealthBar;


    // Start is called before the first frame update
    void Start()
    {

        scoreText.text = "0";
        coinBar.fillAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //SCORE MANAGED HERE
    Coroutine scoreCoroutine = null;
    Coroutine highScoreCoroutine = null;
    public void UpdateScore (int origScore, int newScore, int highScore)
    {

        //Start raising score, but stop it from raising if it was in the process of doing so
        if (scoreCoroutine != null) { StopCoroutine(scoreCoroutine); }
        scoreCoroutine = StartCoroutine(UpdateScoreText(scoreText, origScore, newScore));

        //Do the same for the high score
        Debug.Log(highScore);
        Debug.Log(newScore);
        if (highScore >= newScore) { return; } //If the high score is still higher than the score, then just return
        if (highScoreCoroutine != null) { StopCoroutine(highScoreCoroutine); }
        highScoreCoroutine = StartCoroutine(UpdateScoreText(highScoreText, highScore, newScore));

    }
    public IEnumerator UpdateScoreText(Text t, float origScore, float newScore)
    {

        float counter = 0f;


        while (counter < scoreUpdateTime)
        {
            counter += Time.deltaTime;
            t.text = ((int)Mathf.Lerp(origScore, newScore, counter / scoreUpdateTime)).ToString();
            yield return new WaitForFixedUpdate();
        }

        t.text = ((int) newScore).ToString();

    }
    public void InitializeHighScore (int h)
    {
        highScoreText.text = h.ToString();
    }



    //COIN MANAGED HERE
    Coroutine barCoroutine = null;
    public void UpdateCoin(int coin, int needed)
    {

        float percentage = ((float)coin / (float)needed);

        //Start filling the bar, but stop it from filling if it was in the process of doing so
        if (barCoroutine != null) { StopCoroutine(barCoroutine); } 
        barCoroutine = StartCoroutine(UpdateBar(percentage, coinBar));

    }
    public IEnumerator UpdateBar(float percent, Image bar)
    {

        float origFill = bar.fillAmount;
        float timer = 0f;


        while (timer < barUpdateTime)
        {
            timer += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(origFill, percent, timer / barUpdateTime);
            yield return new WaitForFixedUpdate();
        }

        bar.fillAmount = percent;

    }



    //UPGRADE TEXT MANAGED HERE
    public void DisplayUpgradeText (string rUpgrade, string bUpgrade)
    {
        StartCoroutine(FlashText(rUpgrade, bUpgrade));
    }
    IEnumerator FlashText (string rUpgrade, string bUpgrade)
    {

        float counter = 0;
        float flashCounter = 0;
        //r.material = w;

        redUpgrade.text = rUpgrade;
        redUpgrade.enabled = true;
        blueUpgrade.text = bUpgrade;
        blueUpgrade.enabled = true;

        bool flash = true;

        while (counter < upgradeTextDuration)
        {
            counter++;
            if (flashCounter >= flashDuration)
            {
                if (flash)
                {
                    //r.material = originalMaterial;
                    redUpgrade.enabled = false;
                    blueUpgrade.enabled = false;
                }
                else
                {
                    //r.material = w;
                    redUpgrade.enabled = true;
                    blueUpgrade.enabled = true;
                }
                flash = !flash;
                flashCounter = 0;
            }
            else
            {
                flashCounter++;
            }
            yield return new WaitForFixedUpdate();
        }

        redUpgrade.enabled = false;
        blueUpgrade.enabled = false;

    }


    //TIME MANAGED HERE
    public void UpdateTime (float t)
    {
        timeText.text = TimeToString(t);
    }
    private string TimeToString (float t)
    {
        return ((int)(t / 60)).ToString() + ":" + ((int)(t % 60.0f)).ToString() + ":" + ((int)((t - (int) t) * 100)).ToString();
    }


    //BOSS HEALTH BAR MANAGED HERE
    Coroutine bossCoroutine = null;
    public void LoadBossHealthBar (float time)
    {
        StartCoroutine(InitializeBar(time));
    }
    IEnumerator InitializeBar (float time)
    {

        float counter = 0;

        bossHealthBar.fillAmount = 0;

        while (counter < time)
        {
            bossHealthBar.fillAmount = Mathf.Lerp(0.0f, 1.0f, counter / time);
            counter++;
            yield return new WaitForFixedUpdate();
        }

        bossHealthBar.fillAmount = 1;

    }
    public void UpdateBossHealthBar (float percentage)
    {
        if (bossCoroutine != null) { StopCoroutine(bossCoroutine); }
        bossCoroutine = StartCoroutine(BossBarCoroutine(percentage));
    }
    IEnumerator BossBarCoroutine (float percentage)
    {

        float counter = 0;

        float originalFill = bossHealthBar.fillAmount;
        float bossBarUpdateTime = barUpdateTime * 4;

        while (counter < bossBarUpdateTime)
        {
            bossHealthBar.fillAmount = Mathf.Lerp(originalFill, percentage, counter / bossBarUpdateTime);
            counter++;
            yield return new WaitForFixedUpdate();
        }

        bossHealthBar.fillAmount = percentage;

    }


}
