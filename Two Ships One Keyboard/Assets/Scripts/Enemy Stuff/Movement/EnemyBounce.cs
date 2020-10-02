using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBounce : EnemyMovement
{

    //Maximum speed to accelerate to
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float rampTime;
    private float accelerationCounter = 0;

    //Where to start flying on spawn
    [SerializeField] protected Vector3 startingDirection;
    protected Vector3 currentDirection;

    //How randomly unnatural to bounce after hitting the wall
    [SerializeField] protected float angleRandomizer;

    private float wallCheckLength = 6;
    //Reference to the level, so we can find when to bounce
    //[SerializeField] protected GameObject level;
    private float xBound;
    private float zBound;

    //We will randomize bounce every other bounce
    protected bool randomBounce = true;


    // Start is called before the first frame update
    void Start()
    {
        SetSpawnBounds();
        currentDirection = startingDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If not actionable, don't do anything
        if (!GetComponent<Enemy>().IsActionable()) { return; }

        Accelerate();
        UpdatePosition();
    }


    private void UpdatePosition ()
    {

        UpdateDirection();
        transform.position = Vector3.MoveTowards(transform.position, transform.position + currentDirection * speed * 2, speed * Time.deltaTime);
        return;

    }


    protected virtual void UpdateDirection ()
    {

        Vector3 normal = GetNormal();

        if (normal != Vector3.zero)
        {
            Vector3 d;
            if (randomBounce)
            {
                randomBounce = false;
                d = Quaternion.AngleAxis(Random.Range(-angleRandomizer, angleRandomizer), Vector3.up) * currentDirection;
            }
            else
            {
                randomBounce = true;
                d = currentDirection;
            }

            currentDirection = Vector3.Reflect(d, normal);
            //Have the enemy face where they're going
            transform.LookAt(transform.position + currentDirection * 1);
        }

    }
    protected Vector3 GetNormal ()
    {

        Vector3 checkPosition = transform.position + (wallCheckLength * currentDirection);
        Vector3 normal = Vector3.zero;

        if (transform.position.x + wallCheckLength > xBound)
        {
            normal = new Vector3(-1, 0, 0);
        }
        else if (transform.position.x - wallCheckLength < -xBound)
        {
            normal = new Vector3(1, 0, 0);
        }
        else if (transform.position.z + wallCheckLength > zBound)
        {
            normal = new Vector3(0, 0, -1);
        }
        else if (transform.position.z - wallCheckLength < -zBound)
        {
            normal = new Vector3(0, 0, 1);
        }
        else
        {
            normal = Vector3.zero;
        }

        return normal;

    }


    private void Accelerate ()
    {

        if (accelerationCounter >= rampTime) { return; }

        accelerationCounter++;
        speed = Mathf.Lerp(0, maxSpeed, accelerationCounter / rampTime);

    }


    private bool GetWallCollision ()
    {

        Vector3 checkPosition = transform.position + (wallCheckLength * currentDirection);

        if (checkPosition.x < xBound && checkPosition.x > -xBound && checkPosition.z < zBound && checkPosition.z > -zBound)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    //Set the bounce boundaries, based on the size of the level
    protected void SetSpawnBounds()
    {

        GameObject level = GameObject.FindGameObjectWithTag("Level");

        //Minus 1 to keep things from spawning on the very edge of the level
        xBound = (level.transform.localScale.x / 2) - 1;
        zBound = (level.transform.localScale.z / 2) - 1;

    }


}
