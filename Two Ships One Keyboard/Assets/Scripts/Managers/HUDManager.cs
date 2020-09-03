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


}
