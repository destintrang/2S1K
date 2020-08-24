using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpin : EnemyMovement
{


    [SerializeField] protected Transform toSpin;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If not actionable, don't do anything
        if (!GetComponent<Enemy>().IsActionable()) { return; }

        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
