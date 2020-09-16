using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{


    //GameObject that holds the menu
    [SerializeField] protected ButtonManager pauseScreen;

    private bool paused = false;
    public void TogglePause () { paused = !paused; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPause();
    }


    private void CheckForPause ()
    {
        if (Input.GetButtonDown("Pause") && !paused)
        {
            //Successful pause
            FindObjectOfType<InputManager>().ToggleInputsOff();
            TogglePause();
            pauseScreen.Activate();
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }


    public void OnResumeButton ()
    {

        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        FindObjectOfType<InputManager>().ToggleInputsOn();

    }

    


}
