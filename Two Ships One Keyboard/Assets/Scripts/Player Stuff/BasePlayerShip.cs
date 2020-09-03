using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerShip : BaseShip
{

    protected Rigidbody rb;

    [SerializeField] protected Transform ship;
    [SerializeField] protected MeshRenderer shipMesh;

    [SerializeField] protected float moveSpeed;

    [SerializeField] protected float rotationSmooth;
    float degrees = 0;
    protected float from = 0;
    protected float to = 0;
    float counter = 0;

    //How long the player is invulnerable after barrier
    [SerializeField] protected float iFrames;
    protected bool invincible = false;

    protected Vector3 moveInput = Vector3.zero;
    protected bool rightInput = false;
    protected bool leftInput = false;
    protected bool actionInput = false;


    // Start is called before the first frame update
    void Start()
    {

        //Reference calls
        rb = GetComponent<Rigidbody>();

    }

    private Vector3 speed = Vector3.zero;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float upgradedMaxSpeed ;
    [SerializeField] protected float acceleration;
    [SerializeField] protected float upgradedAcceleration;
    [SerializeField] protected float deceleration ;
    [SerializeField] protected float upgradedDeceleration ;



    // Update is called once per frame
    void Update()
    {

        UpdateInput();

    }



    private void FixedUpdate()
    {

        //Handle actually moving the ship here
        MoveShip();
        UpdateRotation();
        UpdateAction();


    }

    protected void MoveShip ()
    {
        float xSpeed = speed.x;
        float zSpeed = speed.z;
        if (moveInput.x > 0 && speed.x < maxSpeed)
        {
            xSpeed = speed.x + acceleration * Time.deltaTime;
        }
        else if (moveInput.x < 0 && speed.x > -maxSpeed)
        {
            xSpeed = speed.x - acceleration * Time.deltaTime;
        }
        else
        {
            if (speed.x > deceleration * Time.deltaTime)
            {
                xSpeed = speed.x - deceleration * Time.deltaTime;
            }
            else if (speed.x < -deceleration * Time.deltaTime)
            {
                xSpeed = speed.x + deceleration * Time.deltaTime;
            }
            else
            {
                xSpeed = 0;
            }
        }

        if (moveInput.z > 0 && speed.z < maxSpeed)
        {
            zSpeed = speed.z + acceleration * Time.deltaTime;
        }
        else if (moveInput.z < 0 && speed.z > -maxSpeed)
        {
            zSpeed = speed.z - acceleration * Time.deltaTime;
        }
        else
        {
            if (speed.z > deceleration * Time.deltaTime)
            {
                zSpeed = speed.z - deceleration * Time.deltaTime;
            }
            else if (speed.z < -deceleration * Time.deltaTime)
            {
                zSpeed = speed.z + deceleration * Time.deltaTime;
            }
            else
            {
                zSpeed = 0;
            }
        }


        speed = new Vector3(xSpeed, 0, zSpeed);
        transform.position = transform.position + speed * Time.deltaTime;
        //rb.MovePosition(transform.position + speed * Time.deltaTime);
        //rb.velocity(transform.position + speed * Time.deltaTime);
        return;
    }


    //Call this in Update
    protected virtual void UpdateInput ()
    {

        Vector2 input = InputManager.instance.GetP1Movement();
        moveInput = new Vector3(input.x, 0, input.y);

        //rb.velocity = direction * moveSpeed;

    }

    protected virtual void UpdateRotation ()
    {

        if (InputManager.instance.GetP1RotateRight())
        {
            RotateRight();
        }
        else if (InputManager.instance.GetP1RotateLeft())
        {
            RotateLeft();
        }

        ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.Euler(0, to, 0), Time.deltaTime * rotationSmooth);

    }
    private void Rotate(float d)
    {

        //ship.transform.Rotate(0, d, 0);
        degrees += d;
        to = degrees;
        from = ship.transform.rotation.eulerAngles.y;

    }
    protected void RotateRight ()
    {
        Rotate(45.0f);
    }
    protected void RotateLeft()
    {
        Rotate(-45.0f);
    }


    protected virtual void UpdateAction ()
    {



    }

    protected virtual void Upgrade ()
    {

    }


    Coroutine i = null;
    public void StartInvincibility ()
    {
        if (i != null) { StopCoroutine(i); }
        i = StartCoroutine(Invincibility());
    }
    IEnumerator Invincibility ()
    {
        
        float flashInterval = 5;
        float counter = 0;
        float flashCounter = 0;
        //r.material = w;
        shipMesh.enabled = false;
        invincible = true;
        bool flash = true;

        while (counter < iFrames)
        {
            counter++;
            if (flashCounter >= flashInterval)
            {
                if (flash)
                {
                    //r.material = originalMaterial;
                    shipMesh.enabled = true;
                }
                else
                {
                    //r.material = w;
                    shipMesh.enabled = false;
                }
                flash = !flash;
                flashCounter = 0;
            }
            else
            {
                flashCounter++;
            }
            yield return new WaitForFixedUpdate();
        }

        invincible = false;
        shipMesh.enabled = true;

    }


}
