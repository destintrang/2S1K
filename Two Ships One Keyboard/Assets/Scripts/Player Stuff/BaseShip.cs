using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShip : MonoBehaviour
{


    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }


    protected virtual void TakeDamage ()
    {

    }


    //Called when a ship is hit by the blue ship's shockwave
    public virtual void OnShockwave ()
    {

    }

}
