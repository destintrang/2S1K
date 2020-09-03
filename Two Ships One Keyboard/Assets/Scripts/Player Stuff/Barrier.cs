using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{


    [SerializeField] protected Collision.CollisionType color;


    private void OnTriggerEnter(Collider other)
    {
        
        //For some reason it catches itself? so don't let it
        if (other.gameObject.GetInstanceID() == this.gameObject.GetInstanceID()) { return; }

        Collision obj = null;
        if (other.GetComponent<Collision>() != null)
        {
            obj = other.GetComponent<Collision>();
        }
        else { return; }


        if (IsProjectile(other.gameObject))
        {

            //If the bullet is a different color, the barrier is destroyed
            if (obj.GetColor() != color)
            {
                transform.root.GetComponent<BasePlayerShip>().StartInvincibility();
                ToggleOff();
            }

            other.gameObject.GetComponent<Projectile>().OnHit();

        }

        return;

        if (obj.GetColor() == Collision.CollisionType.BLUE)
        {
            if (IsProjectile(other.gameObject))
            {

                Vector3 direction = transform.forward;
                other.gameObject.GetComponent<Projectile>().ReflectProjectile(direction);
                //Play reflect sound effect here
                FindObjectOfType<AudioManager>().Play("Reflect");

            }
        }

        //Catch every bullet
        //Destroy(other.gameObject);

    }


    //Checks to make sure the object that hit the barrier is a projectile
    private bool IsProjectile(GameObject o)
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


    public void ToggleOn()
    {
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
    public void ToggleOff()
    {
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }



}
