using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{


    protected Vector3 GetTargetLocation(Targeting.TargetPlayer target)
    {

        if (target == Targeting.TargetPlayer.RED)
        {
            return GameObject.FindGameObjectWithTag("RED").transform.position;
        }
        else if (target == Targeting.TargetPlayer.BLUE)
        {
            return GameObject.FindGameObjectWithTag("BLUE").transform.position;
        }
        else
        {
            return Vector3.zero;
        }

    }


    //Used to calculate randomized delays
    protected float DelayRandomizer (float delay, float randomizer)
    {
        return Random.Range(delay * (1 - randomizer), delay * (1 + randomizer));
    }
    //Used to simulate inaccuracy
    protected Vector3 AccuracyRandomizer (Vector3 dir, float degrees)
    {

        return Quaternion.AngleAxis(Random.Range(-degrees, degrees), Vector3.up) * dir;
        return Vector3.zero;

    }


}
