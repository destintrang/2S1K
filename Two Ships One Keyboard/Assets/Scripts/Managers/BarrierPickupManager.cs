using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPickupManager : MonoBehaviour
{


    //Pickup prefab
    [SerializeField] protected GameObject pickUp;

    //Chance that a barrier pickup will be spawned
    [Range(0, 1.0f)]
    [SerializeField] protected float chance;


    //How long coins stay on the level for
    [SerializeField] protected float duration;


    public void SpawnBarrier (Vector3 location)
    {

        if (Random.Range(0.0f, 1.0f) < chance)
        {
            BarrierPickup b = Instantiate(pickUp, location, Quaternion.identity).GetComponent<BarrierPickup>();
            StartCoroutine(b.Duration(duration));
        }

    }


}
