using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseShip : BasePlayerShip
{



    //Move speed when shield is held up
    [SerializeField] protected float shieldSpeedMult;
    private float baseSpeed;

    //Reference to the animator controlling the shield
    [SerializeField] protected Animator shield;

    private bool shielding = false;

    [SerializeField] protected float dischargeTime;
    [SerializeField] protected float dischargeCooldown;
    private float dischargeCounter = 0;
    private float cooldownCounter = 0;



    void Start()
    {

        //Reference calls
        rb = GetComponent<Rigidbody>();
        baseSpeed = moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        UpdateMovement();
        UpdateRotation();
        UpdateAction();

    }


    //Blue ship will die to non-blue bullets
    protected override void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collision>() != null && other.GetComponent<Collision>().GetColor() != Collision.CollisionType.BLUE)
        {

            Debug.Log("OW");
            //Blue ship got hit
            TakeDamage();

        }

    }
    //Blue ship will die to non-blue bodies
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {

        if (collision.gameObject.GetComponentInChildren<Collision>() != null && collision.gameObject.GetComponentInChildren<Collision>().GetColor() != Collision.CollisionType.BLUE)
        {

            //Blue ship got hit
            TakeDamage();

        }

    }


    protected override void TakeDamage()
    {
        GameManager.instance.LoseGame();
    }


    protected override void UpdateMovement()
    {

        Vector2 input = InputManager.instance.GetP2Movement();
        moveInput = new Vector3(input.x, 0, input.y);

        //rb.velocity = direction * moveSpeed;

    }

    protected override void UpdateRotation()
    {

        if (InputManager.instance.GetP2RotateRight())
        {
            RotateRight();
        }
        else if (InputManager.instance.GetP2RotateLeft())
        {
            RotateLeft();
        }

        ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.Euler(0, to, 0), Time.deltaTime * rotationSmooth);

    }


    protected override void UpdateAction()
    {

        //Discharge counter always will increase
        dischargeCounter += Time.deltaTime;

        //Discharge cooldown will always decrease
        if (cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;
        }

        //If the player presses the action button
        if (InputManager.instance.GetP2Action())
        {

            //If the player is in cooldown, just return
            if (cooldownCounter > 0)
            {
                return;
            }

            if (dischargeCounter < dischargeTime)
            {

                //Tried to activate the shield too soon, caused a discharge
                Debug.Log("DISCHARGE");
                cooldownCounter = dischargeCooldown;

                //No matter what the shield is doing, it will turn off
                shield.Play("Discharge");

                //Revert things
                shielding = false;
                moveSpeed = baseSpeed;

                return;

            }

            //If the player pressed the action button, it will always reset the counter to 0
            dischargeCounter = 0;

            if (!shielding)
            {  
                shield.Play("Start Shield");
                shielding = true;

                //Slow the player when shield is held
                moveSpeed *= shieldSpeedMult;
            }
            else if (shielding)
            {
                shield.Play("End Shield");
                shielding = false;

                //Revert speed
                moveSpeed = baseSpeed;
            }

        }

    }


}
