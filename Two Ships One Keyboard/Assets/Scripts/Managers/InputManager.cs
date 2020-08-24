using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    private bool disabled = false;
    public void ToggleInputsOff() { disabled = true; }
    public void ToggleInputsOn() { disabled = false; }


    //Singleton
    public static InputManager instance;
    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {

    }


    public Vector2 GetP1Movement ()
    {

        if (disabled) { return new Vector2(); }
        
        else
        {
            return new Vector2(Input.GetAxis("P1X"), Input.GetAxis("P1Y"));
        }

    }
    public Vector2 GetP2Movement()
    {

        if (disabled) { return new Vector2(); }

        else
        {
            return new Vector2(Input.GetAxis("P2X"), Input.GetAxis("P2Y"));
        }

    }

    public bool GetP1RotateRight()
    {

        if (Input.GetKeyDown(KeyCode.E)) { return true; }
        else return false;

    }
    public bool GetP1RotateLeft()
    {

        if (Input.GetKeyDown(KeyCode.Q)) { return true; }
        else return false;

    }
    public bool GetP2RotateRight()
    {

        if (Input.GetKeyDown(KeyCode.O)) { return true; }
        else return false;

    }
    public bool GetP2RotateLeft()
    {

        if (Input.GetKeyDown(KeyCode.U)) { return true; }
        else return false;

    }


    public bool GetP1Action()
    {

        if (Input.GetKey(KeyCode.Space)) { return true; }
        else return false;

    }
    public bool GetP2Action()
    {

        if (Input.GetKeyDown(KeyCode.H)) { return true; }
        else return false;

    }


}
