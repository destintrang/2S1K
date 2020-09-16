using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{


    public delegate void AfterFunction();
    private AfterFunction postTransitionCall;

    Animator a;

    //Function to be called after a transition
    //private UnityEvent postTransitionCall;


    private void Awake()
    {
        a = GetComponent<Animator>();
    }

    public void PanDownToBlack(float speed = 1, AfterFunction e = null)
    {
        postTransitionCall = e;
        a.speed = speed;
        a.Play("Pan Down To Black");
    }
    public void PanDownToClear(float speed = 1, AfterFunction e = null)
    {
        postTransitionCall = e;
        a.speed = speed;
        a.Play("Pan Down To Clear");
    }


    public void PostTransitionCall ()
    {
        if (postTransitionCall == null) { return; }
        postTransitionCall.Invoke();
    }


    


}
