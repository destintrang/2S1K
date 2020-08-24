﻿using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    

    [SerializeField] protected Color red;
    [SerializeField] protected Color blue;
    [SerializeField] protected Color purple;

    [SerializeField] protected ParticleSystem redTrail;
    [SerializeField] protected ParticleSystem blueTrail;
    [SerializeField] protected ParticleSystem purpleTrail;

    [SerializeField] protected float sparkDuration;
    [SerializeField] protected ParticleSystem sparks;

    private Collision collision;

    private Vector3 direction = new Vector3();
    private float speed = 0;




    private void Awake ()
    {

        //Reference calls
        collision = GetComponent<Collision>();

    }


    private void Update()
    {

        transform.Translate(speed * direction * Time.deltaTime);

    }


    public void StartProjectile (Collision.CollisionType color, Vector3 dir, float sp)
    {

        //Just deactivate all trails here to make things easy
        blueTrail.gameObject.SetActive(false);
        redTrail.gameObject.SetActive(false);
        purpleTrail.gameObject.SetActive(false);


        direction = dir;
        speed = sp;

        Color actualColor = Color.white;
        if (color == Collision.CollisionType.BLUE) { 
            actualColor = blue;
            blueTrail.gameObject.SetActive(true);
        }
        else if (color == Collision.CollisionType.RED) { 
            actualColor = red;
            redTrail.gameObject.SetActive(true);
        }
        else if (color == Collision.CollisionType.PURPLE) { 
            actualColor = purple;
            purpleTrail.gameObject.SetActive(true);
        }

        collision.SetColor(color, actualColor);

        //Start the projectile duration
        StartCoroutine(ProjectileDuration());

    }


    IEnumerator ProjectileDuration ()
    {

        float timer = 0f;

        while (timer < 10f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        DestroyProjectile();

    }
    public void DestroyProjectile ()
    {
        ProjectileManager.instance.AddToPool(this);
    }


    //Called when a projectile gets reflected by, say, the blue shield
    public void ReflectProjectile (Vector3 normal)
    {
        direction = Vector3.Reflect(direction, normal);
    }



    public void OnHit ()
    {

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        FindObjectOfType<SparksManager>().Spark(transform.position, direction);
        DestroyProjectile();

    }


    public void Activate ()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate ()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }


}
