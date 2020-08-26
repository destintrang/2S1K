using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveHitbox : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        //For some reason it catches itself? so don't let it
        if (other.gameObject.GetInstanceID() == this.gameObject.GetInstanceID()) { return; }

        Collision obj = other.GetComponent<Collision>();

        if (obj == null)
        {
            return;
        }


        //Shockwave hurts anything that isn't blue
        if (obj.GetColor() != Collision.CollisionType.BLUE)
        {
            other.GetComponent<BaseShip>().OnShockwave();
        }

        //Catch every bullet
        //Destroy(other.gameObject);

    }


}
