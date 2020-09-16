using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{


    //Bool telling whether this is active
    protected bool active = false;


    [SerializeField] protected string posButton;
    [SerializeField] protected string negButton;


    //Stuff for the selector
    [SerializeField] protected RectTransform selector;
    [SerializeField] protected List<RectTransform> options;
    protected int optionIndex = 0;
    //Offset of where the selector will be relative to the button
    [SerializeField] protected Vector3 offset = new Vector3(-80, 0, 0);

    //To handle fast inputs
    protected float inputCooldown = 8;
    protected float cooldownCounter = 0;






    // Start is called before the first frame update
    void Start()
    {
        //if (startActive) { Activate(); }
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckForPause();
        CheckForJoystickMovement();
        CheckForSubmitButton();
    }
    void FixedUpdate()
    {
        Debug.Log("sajdapso");
        //Update counter
        if (cooldownCounter < inputCooldown)
        {
            cooldownCounter += 1;
            return;
        }
    }



    //private void CheckForPause()
    //{
    //    if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Pause"))
    //    {
    //        TogglePause();
    //    }
    //}

    protected virtual void CheckForJoystickMovement()
    {

        //If there's only 1 option, theres no need for button navi
        if (options.Count < 2) { return; }

        //If button navi is on cooldown, return
        if (!active) { return; }

        if (cooldownCounter < inputCooldown)
        {
            return;
        }

        //Check for joystick input
        int input = GetInput();
        if (input < 0)
        {
            Debug.Log("sajdapso");
            MoveSelector(1);
            //MenuSFXManager.instance.PlayButtonNavi();
            cooldownCounter = 0;
        }
        else if (input > 0)
        {
            Debug.Log("sajdapso");
            MoveSelector(-1);
            //MenuSFXManager.instance.PlayButtonNavi();
            cooldownCounter = 0;
        }


    }
    private void CheckForSubmitButton()
    {

        if (!active) { return; }

        //Check for submit input
        if (GetSubmitButton())
        {
            OnSubmitButton();
            //MenuSFXManager.instance.PlayButtonSubmit();
            cooldownCounter = 0;
        }

    }


    public void Activate()
    {

        //Toggle pause on
        active = true;
        //Set up the UI
        ResetSelector();

        return;

        if (!active)
        {
            //Toggle pause on
            active = true;
            //Set up the UI
            ResetSelector();
        }
        else if (active)
        {
            //Toggle pause off
            active = false;
        }

    }




    private void OnSubmitButton()
    {

        options[optionIndex].GetComponent<Button>().onClick.Invoke();

    }


    private void ResetSelector()
    {

        optionIndex = 0;
        selector.localPosition = new Vector3(options[optionIndex].localPosition.x, options[optionIndex].localPosition.y, options[optionIndex].localPosition.z) + offset;

    }
    protected void MoveSelector(int direction)
    {

        optionIndex += direction;
        if (optionIndex < 0) { optionIndex = options.Count - 1; }
        else if (optionIndex >= options.Count) { optionIndex = 0; }

        selector.localPosition = new Vector3(options[optionIndex].localPosition.x, options[optionIndex].localPosition.y, options[optionIndex].localPosition.z) + offset;

    }


    public void Recalibrate()
    {

        selector.localPosition = new Vector3(options[optionIndex].localPosition.x, options[optionIndex].localPosition.y, options[optionIndex].localPosition.z) + offset;

    }



    public virtual int GetInput()
    {


        if (Input.GetButton(posButton)) { return 1; }
        else if (Input.GetButton(negButton)) { return -1; }
        else { return 0; }


    }
    public bool GetSubmitButton()
    {
        return Input.GetButtonDown("Submit");
    }

}