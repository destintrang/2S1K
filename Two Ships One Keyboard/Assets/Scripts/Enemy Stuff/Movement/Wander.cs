using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : EnemyMovement
{


    //Target location the enemy will wander towards
    private Vector3 target;

    //How long the enemy should wander towards a certain duration for
    [SerializeField] protected float wanderDuration;
    private float durationCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //If not actionable, don't do anything
        if (!GetComponent<Enemy>().IsActionable()) { return; }

        UpdateTarget();
        UpdatePosition();

    }


    void UpdateTarget ()
    {

        //Only change target position once we've wandered enough
        durationCounter += 1;
        if (durationCounter < wanderDuration) { return; }

        //Change target to a new random target
        target = GetRandomTarget();

        //Reset the counter
        durationCounter = 0;

    }

    void UpdatePosition ()
    {

        //Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }


    private Vector3 GetRandomTarget ()
    {

        Vector3 randomPos = Random.insideUnitSphere * 100;
        randomPos = transform.position + new Vector3(randomPos.x, 0, randomPos.z);
        return randomPos;

    }




}
