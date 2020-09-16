using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is for managing everything in the UI
public class HUDManager : MonoBehaviour
{


    [SerializeField] protected Text scoreText;
    [SerializeField] protected float scoreUpdateTime;


    [SerializeField] protected Image coinBar;
    [SerializeField] protected float barUpdateTime;

    [SerializeField] protected Text redUpgrade;
    [SerializeField] protected Text blueUpgrade;
    [SerializeField] protected float upgradeTextDuration;
    private float flashDuration = 10;


    [SerializeField] protected Text timeText;


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
    public void UpdateScore (int origScore, int newScore)
    {
        //Start raising score, but stop it from raising if it was in the process of doing so
        if (scoreCoroutine != null) { StopCoroutine(scoreCoroutine); }
        scoreCoroutine = StartCoroutine(UpdateScoreText(origScore, newScore));
    }
    public IEnumerator UpdateScoreText(float origScore, float newScore)
    {

        float timer = 0f;


        while (timer < scoreUpdateTime)
        {
            timer += Time.deltaTime;
            scoreText.text = ((int)Mathf.Lerp(origScore, newScore, timer / scoreUpdateTime)).ToString();
            yield return new WaitForFixedUpdate();
        }

        scoreText.text = ((int) newScore).ToString();

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


}
