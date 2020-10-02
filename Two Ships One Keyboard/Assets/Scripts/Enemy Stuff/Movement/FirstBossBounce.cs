using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ShootAround))]
public class FirstBossBounce : EnemyBounce
{


    [SerializeField] protected float initialSpeed;
    [SerializeField] protected float finalSpeed;

    //To prevent multiple fires too quickly
    private int shootBuffer = 7;
    private int shootCounter = 0;


    private void Start()
    {
        initialSpeed = maxSpeed;
        SetSpawnBounds();
        currentDirection = startingDirection;
    }


    protected override void UpdateDirection()
    {

        UpdateSpeed();

        shootCounter++;
        Vector3 normal = GetNormal();

        //Boss has hit the wall
        if (normal != Vector3.zero)
        {
            
            if (shootCounter >= shootBuffer)
            {
                ShootRandomColorAround();
                shootCounter = 0;
            }


            Vector3 d;
            if (randomBounce)
            {
                randomBounce = false;
                float a = GetRandomBounceAngle();
                currentDirection = Vector3.Reflect(currentDirection, normal);
                currentDirection = Quaternion.AngleAxis(a, Vector3.up) * currentDirection;
                //d = Quaternion.AngleAxis(a, Vector3.up) * currentDirection;
                Debug.Log(a);
            }
            else
            {
                currentDirection = Vector3.Reflect(currentDirection, normal);
            }
            //currentDirection = Vector3.Reflect(d, normal);
            //Have the enemy face where they're going
            transform.LookAt(transform.position + currentDirection * 1);
        }

    }
    //Boss gets faster the closer it is to death
    private void UpdateSpeed ()
    {

        speed = Mathf.Lerp(initialSpeed, finalSpeed, 1 - GetComponent<Enemy>().GetHealthPercentage());

    }


    private void ShootRandomColorAround ()
    {

        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            GetComponent<ShootAround>().ShootAroundEnemy(Collision.CollisionType.BLUE);
        }
        else
        {
            GetComponent<ShootAround>().ShootAroundEnemy(Collision.CollisionType.RED);
        }

    }


    private float GetRandomBounceAngle ()
    {
        
        //Limit appears to be 28
        float angle = Random.Range(25, 28);
        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            return -angle;
        }
        else
        {
            return angle;
        }

    }



}
