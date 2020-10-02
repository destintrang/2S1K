using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public bool godMode;

    [SerializeField] protected GameObject winScreen;
    [SerializeField] protected GameObject loseScreen;

    //Reference to transforms for moving the players in the opening
    [SerializeField] protected Transform redShip;
    [SerializeField] protected Transform blueShip;

    private Vector3 redPosA = new Vector3(-3.5f, 0, -25);
    private Vector3 redPosB = new Vector3(-3.5f, 0, 0);
    private Vector3 bluePosA = new Vector3(3.5f, 0, -25);
    private Vector3 bluePosB = new Vector3(3.5f, 0, 0);

    //Delay before players are spawned
    [SerializeField] protected float firstDelay;
    //Delay before players are given control
    [SerializeField] protected float secondDelay;
    //Delay before enemies are spawned
    [SerializeField] protected float thirdDelay;


    //Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(StartSequence());
    }


    public void WinGame()
    {

        winScreen.SetActive(true);

    }


    public void LoseGame ()
    {

        if (godMode) {
            Debug.Log("DEATH");
            return; 
        }

        //Lose the game

        FindObjectOfType<ScoreManager>().StopTimer();
        DisableEverything();
        StopAllCoroutines();

        if (FindObjectOfType<StatsManager>() != null)
        {
            FindObjectOfType<StatsManager>().UpdateHighScore(FindObjectOfType<ScoreManager>().GetScore());
        }

        loseScreen.SetActive(true);
        loseScreen.GetComponent<ButtonManager>().Activate();

    }


    //This quit method is only called through the pause menu or after death
    //bool determines if this was called through the pause menu
    public void QuitGame (bool pause)
    {

        DisableEverything();
        StopAllCoroutines();


        Time.timeScale = 1;
        if (pause)
        {
            FindObjectOfType<PauseButtonManager>().gameObject.SetActive(false);
            //Update the high score here
            FindObjectOfType<StatsManager>().UpdateHighScore(FindObjectOfType<ScoreManager>().GetScore());
        }
        FindObjectOfType<Transition>().PanDownToBlack(0.3f, GoToMenu);


    }
    public void GoToMenu ()
    {
        SceneManager.LoadSceneAsync("Menu");
    }



    public void OnReplayButton()
    {
        DisableEverything();
        StopAllCoroutines();
        Time.timeScale = 1;
        FindObjectOfType<Transition>().PanDownToBlack(0.3f, RestartLevel);
    }
    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }



    //Call this when the game is over (via quitting or dying)
    public void DisableEverything ()
    {

        FindObjectOfType<EnemyWaveManager>().enabled = false;
        FindObjectOfType<InputManager>().ToggleInputsOff();

        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {

            EnemyMovement m = e.GetComponent<EnemyMovement>();
            if (m != null) m.enabled = false;
            EnemyAttack a = e.GetComponent<EnemyAttack>();
            if (a != null) a.enabled = false;

        }

        foreach (Projectile p in FindObjectsOfType<Projectile>())
        {
            p.enabled = false;
        }

        foreach (BasePlayerShip s in FindObjectsOfType<BasePlayerShip>())
        {
            s.RemoveVelocity();
        }

    }



    IEnumerator StartSequence ()
    {


        FindObjectOfType<Transition>().PanDownToClear(0.3f);

        //Delay before players are spawned (stars are spawned)
        float counter = 0;

        //Hide the players
        redShip.gameObject.SetActive(false);
        blueShip.gameObject.SetActive(false);

        //Disable player input
        FindObjectOfType<InputManager>().ToggleInputsOff();
        //Disable enemies from spawning
        FindObjectOfType<EnemyWaveManager>().enabled = false;

        while (counter < firstDelay)
        {
            counter++;
            yield return new WaitForFixedUpdate();
        }



        //Delay before players are given control (spawn and bring players to center here)
        counter = 0;

        //Unhide the players and spawn them
        redShip.gameObject.SetActive(true);
        blueShip.gameObject.SetActive(true);
        redShip.GetComponent<Animator>().Play("Player Spawn");
        blueShip.GetComponent<Animator>().Play("Player Spawn");
        redShip.transform.position = redPosA;
        blueShip.transform.position = bluePosA;

        //Lerp the players' positions
        while (counter < secondDelay)
        {
            redShip.position = Vector3.Lerp(redPosA, redPosB, counter / secondDelay);
            blueShip.position = Vector3.Lerp(bluePosA, bluePosB, counter / secondDelay);
            counter++;
            yield return new WaitForFixedUpdate();
        }

        //Enable player input
        FindObjectOfType<InputManager>().ToggleInputsOn();
        //Start the game timer: the game has begun!
        FindObjectOfType<ScoreManager>().StartTimer();



        //Delay before enemies are spawned
        counter = 0;

        while (counter < thirdDelay)
        {
            counter++;
            yield return new WaitForFixedUpdate();
        }

        //Enable enemy spawns
        FindObjectOfType<EnemyWaveManager>().enabled = true;



    }


}
