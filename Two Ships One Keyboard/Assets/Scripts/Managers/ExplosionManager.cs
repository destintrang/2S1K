using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : GameobjectPool
{
    [SerializeField] protected float deactivateTime;


    public void Explode(Vector3 location)
    {

        GameObject e = RequestObject(location);
        StartCoroutine(PlayEffect(e.GetComponent<ParticleSystem>()));

    }

    IEnumerator PlayEffect(ParticleSystem e)
    {

        float counter = 0;
        e.Stop();
        e.Play();

        while (counter <= deactivateTime)
        {
            yield return null;
        }

        e.Stop();
        AddToPool(e.gameObject);

    }
}
