using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        //For some reason it catches itself? so don't let it
        if (other.gameObject.GetInstanceID() == this.gameObject.GetInstanceID()) { return; }

        Collision obj = null;
        if (other.GetComponent<Collision>() != null)
        {
            obj = other.GetComponent<Collision>();
        }


        if (obj.GetColor() == Collision.CollisionType.BLUE)
        {
            if (IsBlockable(other.gameObject))
            {
                Vector3 direction = transform.forward;
                other.gameObject.GetComponent<Projectile>().ReflectProjectile(direction);
            }
        }

        //Catch every bullet
        //Destroy(other.gameObject);

    }

    //Checks to make sure the object that hit the shield is a projectile
    private bool IsBlockable(GameObject o)
    {

        Projectile p = o.GetComponent<Projectile>();
        if (p != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
