using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is for seperate hitboxes that are linked to an enemy (that isn't linked to this object)
public class EnemyExtension : MonoBehaviour
{


    [SerializeField] protected Enemy main;


    protected void OnTriggerEnter(Collider other)
    {

        main.OnProjectileEnter(other);

    }
}
