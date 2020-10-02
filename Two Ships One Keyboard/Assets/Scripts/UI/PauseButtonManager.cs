using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used instead of the normal button manager, to be allowed to use while time is stopped
public class PauseButtonManager : ButtonManager
{

    protected override void CheckForJoystickMovement()
    {

        //If there's only 1 option, theres no need for button navi
        if (options.Count < 2) { return; }

        //If button navi is on cooldown, return
        if (!active) { return; }


        //Check for joystick input
        int input = GetInput();
        if (input < 0)
        {
            MoveSelector(1);
            //MenuSFXManager.instance.PlayButtonNavi();
            cooldownCounter = 0;
        }
        else if (input > 0)
        {
            MoveSelector(-1);
            //MenuSFXManager.instance.PlayButtonNavi();
            cooldownCounter = 0;
        }


    }


    protected override void MoveSelector(int direction)
    {

        optionIndex += direction;
        if (optionIndex < 0) { optionIndex = options.Count - 1; }
        else if (optionIndex >= options.Count) { optionIndex = 0; }

        //if (selectorCoroutine != null) { StopCoroutine(selectorCoroutine); }
        //selectorCoroutine = StartCoroutine(SelectorLerp(new Vector3(options[optionIndex].localPosition.x, options[optionIndex].localPosition.y, options[optionIndex].localPosition.z) + offset));
        selector.localPosition = new Vector3(options[optionIndex].localPosition.x, options[optionIndex].localPosition.y, options[optionIndex].localPosition.z) + offset;

    }


    public override int GetInput()
    {


        if (Input.GetButtonDown(posButton)) { return 1; }
        else if (Input.GetButtonDown(negButton)) { return -1; }
        else { return 0; }


    }


}
