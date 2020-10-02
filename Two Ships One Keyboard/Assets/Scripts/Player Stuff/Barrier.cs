using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    private bool active = false;
    public bool IsActive () { return active; }

    [SerializeField] protected Collision.CollisionType color;
    [SerializeField] protected MeshRenderer mesh;

    [SerializeField] protected float barrierDeployTime;
    [SerializeField] protected float minFresnel;
    [SerializeField] protected float maxFresnel;


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
            if (other.gameObject.GetComponent<Projectile>().IsOwner(transform.root.gameObject))
            {
                return;
            }

            //If the bullet is a different color, the barrier is destroyed
            if (obj.GetColor() != color)
            {
                transform.root.GetComponent<BasePlayerShip>().StartInvincibility();
                ToggleOff();
            }

            other.gameObject.GetComponent<Projectile>().OnHit();

        }

        else if (obj.GetColor() != color)
        {
            transform.root.GetComponent<BasePlayerShip>().StartInvincibility();
            ToggleOff();
        }

        return;

        if (obj.GetColor() == Collision.CollisionType.BLUE)
        {
            if (IsProjectile(other.gameObject))
            {

                Vector3 direction = transform.forward;
                other.gameObject.GetComponent<Projectile>().ReflectProjectile(direction);
                //Play reflect sound effect here
                FindObjectOfType<AudioManager>().PlaySoundEffect("Reflect");

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


    Coroutine fade = null;
    public void ToggleOn()
    {
        active = true;
        if (fade != null) { StopCoroutine(fade); }
        fade = StartCoroutine(FadeIn());
    }
    public void ToggleOff()
    {
        active = false;
        if (fade != null) { StopCoroutine(fade); }
        fade = StartCoroutine(FadeOut());
    }


    IEnumerator FadeIn()
    {

        GetComponent<SphereCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        mesh.material.SetFloat("Vector1_B2BD2876", maxFresnel);
        float counter = 0;

        while (counter < barrierDeployTime)
        {
            counter++;
            mesh.material.SetFloat("Vector1_B2BD2876", Mathf.Lerp(maxFresnel, minFresnel, counter / barrierDeployTime));
            yield return new WaitForEndOfFrame();
        }

        mesh.material.SetFloat("Vector1_B2BD2876", minFresnel);

    }
    IEnumerator FadeOut()
    {

        GetComponent<SphereCollider>().enabled = false;
        mesh.material.SetFloat("Vector1_B2BD2876", minFresnel);
        float counter = 0;

        while (counter < barrierDeployTime)
        {
            counter++;
            mesh.material.SetFloat("Vector1_B2BD2876", Mathf.Lerp(minFresnel, maxFresnel, counter / barrierDeployTime));
            yield return new WaitForEndOfFrame();
        }

        mesh.material.SetFloat("Vector1_B2BD2876", maxFresnel);
        GetComponent<MeshRenderer>().enabled = false;

    }
    private void Start()
    {
        //StartCoroutine(FadeIn());
    }



}
