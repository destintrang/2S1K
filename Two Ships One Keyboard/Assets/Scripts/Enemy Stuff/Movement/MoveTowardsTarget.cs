using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : EnemyMovement
{


    //Who we want to move towards
    private Targeting.TargetPlayer target;



    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Targeting>().GetTarget();
        Vector3 targetPos = GetTargetLocation(target);
        transform.LookAt(targetPos);
    }

    // Update is called once per frame
    void Update()
    {

        //If not actionable, don't do anything
        if (!GetComponent<Enemy>().IsActionable()) { return; }

        UpdatePosition();

    }


    void UpdatePosition ()
    {

        //Get target position based on target
        Vector3 targetPos = GetTargetLocation(target);

        //Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        //Face the target
        transform.LookAt(targetPos);

    }


}
