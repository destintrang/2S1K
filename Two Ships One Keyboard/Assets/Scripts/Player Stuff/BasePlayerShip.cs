using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerShip : BaseShip
{

    protected Rigidbody rb;

    [SerializeField] protected Transform ship;

    protected Vector3 moveInput;
    [SerializeField] protected float moveSpeed;

    [SerializeField] protected float rotationSmooth;
    float degrees = 0;
    protected float from = 0;
    protected float to = 0;
    float counter = 0;


    // Start is called before the first frame update
    void Start()
    {

        //Reference calls
        rb = GetComponent<Rigidbody>();

    }

    [SerializeField] protected Vector3 speed = Vector3.zero;
    [SerializeField] protected Vector3 maxSpeed = new Vector3(100, 0, 100);
    [SerializeField] protected Vector3 acceleration = new Vector3(100, 0, 100);
    [SerializeField] protected Vector3 deceleration = new Vector3(50, 0, 50);
    // Update is called once per frame
    void Update()
    {

        UpdateMovement();
        UpdateRotation();
        UpdateAction();

        MoveShip();

    }



    private void FixedUpdate()
    {

        //Handle actually moving the ship here
        MoveShip();


    }

    private void MoveShip ()
    {
        float xSpeed = speed.x;
        float zSpeed = speed.z;
        if (moveInput.x > 0 && speed.x < maxSpeed.x)
        {
            xSpeed = speed.x + acceleration.x * Time.deltaTime;
        }
        else if (moveInput.x < 0 && speed.x > -maxSpeed.x)
        {
            xSpeed = speed.x - acceleration.x * Time.deltaTime;
        }
        else
        {
            if (speed.x > deceleration.x * Time.deltaTime)
            {
                xSpeed = speed.x - deceleration.x * Time.deltaTime;
            }
            else if (speed.x < -deceleration.x * Time.deltaTime)
            {
                xSpeed = speed.x + deceleration.x * Time.deltaTime;
            }
            else
            {
                xSpeed = 0;
            }
        }

        if (moveInput.z > 0 && speed.z < maxSpeed.z)
        {
            zSpeed = speed.z + acceleration.z * Time.deltaTime;
        }
        else if (moveInput.z < 0 && speed.z > -maxSpeed.z)
        {
            zSpeed = speed.z - acceleration.z * Time.deltaTime;
        }
        else
        {
            if (speed.z > deceleration.z * Time.deltaTime)
            {
                zSpeed = speed.z - deceleration.z * Time.deltaTime;
            }
            else if (speed.z < -deceleration.z * Time.deltaTime)
            {
                zSpeed = speed.z + deceleration.z * Time.deltaTime;
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


    protected virtual void UpdateMovement ()
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


}
