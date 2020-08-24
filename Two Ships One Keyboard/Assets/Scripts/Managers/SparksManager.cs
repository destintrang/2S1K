using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksManager : GameobjectPool
{


    [SerializeField] protected float deactivateTime;


    public void Spark(Vector3 location, Vector3 direction)
    {

        GameObject spark = RequestObject(location);
        spark.transform.LookAt(spark.transform.position + direction);
        StartCoroutine(PlayEffect(spark.GetComponent<ParticleSystem>()));

    }

    IEnumerator PlayEffect (ParticleSystem spark)
    {

        float counter = 0;
        spark.Stop();
        spark.Play();

        while (counter <= deactivateTime)
        {
            yield return null;
        }

        spark.Stop();
        AddToPool(spark.gameObject);

    }


}
