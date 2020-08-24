using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{



    [SerializeField] protected float speed;



    protected Vector3 GetTargetLocation (Targeting.TargetPlayer target)
    {

        if (target == Targeting.TargetPlayer.RED)
        {
            return GameObject.FindGameObjectWithTag("RED").transform.position;
        }
        else if (target == Targeting.TargetPlayer.BLUE)
        {
            return GameObject.FindGameObjectWithTag("BLUE").transform.position;
        }
        else if (target == Targeting.TargetPlayer.BOTH)
        {

            //Compare the distances of the two ships
            float distanceRed = Vector3.Distance(GameObject.FindGameObjectWithTag("RED").transform.position, transform.position);
            float distanceBlue = Vector3.Distance(GameObject.FindGameObjectWithTag("BLUE").transform.position, transform.position);

            //Choose the ship that's closest
            if (distanceRed < distanceBlue)
            {
                return GameObject.FindGameObjectWithTag("RED").transform.position;
            }
            else if (distanceBlue <= distanceRed)
            {
                return GameObject.FindGameObjectWithTag("BLUE").transform.position;
            }
            else { return Vector3.zero; }

        }
        else { return Vector3.zero; }

    }

    public void DisableMovement ()
    {
        this.enabled = false;
    }

}
