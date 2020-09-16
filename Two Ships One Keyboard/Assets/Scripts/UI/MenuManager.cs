using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{




    //The first screen to start the scene on
    [SerializeField] protected Canvas currentCanvas;
    [SerializeField] protected ButtonManager currentButtonManager;


    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Transition>().PanDownToClear(0.7f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void StartGameButton ()
    {
        FindObjectOfType<Transition>().PanDownToBlack(0.7f, StartGame);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }


    //Functions used to switch menu screens
    //Put both? of these onto buttons
    public void SwitchCanvas(Canvas c)
    {
        currentCanvas.gameObject.SetActive(false);
        c.gameObject.SetActive(true);
        currentCanvas = c;
    }
    public void SwithButtonManager(ButtonManager b)
    {
        currentButtonManager = b;
        currentButtonManager.Recalibrate();
    }


    //public void UpdateStats ()
    //{
    //    StatsManager s = StatsManager.instance;

    //    runsText.text = s.GetRuns().ToString();
    //    stepsText.text = s.GetSteps().ToString();
    //    eggsHighscoreText.text = s.GetEggsHighscore().ToString();
    //    eggsTotalText.text = s.GetEggsTotal().ToString();
    //    eggsAverageText.text = ((Mathf.Round(s.GetEggsAverage() * 100)) / 100.0).ToString();
    //    pointsHighscoreText.text = s.GetPointsHighscore().ToString();
    //    pointsAverageText.text = ((Mathf.Round(s.GetPointsAverage() * 100)) / 100.0).ToString();
    //    jumpedOverText.text = s.GetJumped().ToString();
    //    swamUnderText.text = s.GetSwam().ToString();
    //}


}