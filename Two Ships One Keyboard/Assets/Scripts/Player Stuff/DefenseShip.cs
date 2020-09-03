using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseShip : BasePlayerShip
{



    //Move speed when shield is held up
    [SerializeField] protected float shieldSpeedMult;
    private float baseSpeed;

    //Reference to the script controlling the shield
    [SerializeField] protected ShieldController shield;

    private bool shielding = false;

    [SerializeField] protected float dischargeTime;
    [SerializeField] protected float dischargeCooldown;
    [SerializeField] protected float upgradedDischargeCooldown;
    [SerializeField] protected float shieldDeploySpeed;
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

        UpdateInput();
        UpdateRotation();
        UpdateAction();

    }

    private void FixedUpdate()
    {

        //Handle actually moving the ship here
        MoveShip();
        UpdateShieldCooldowns();


    }

    //Blue ship will die to non-blue bullets
    protected override void OnTriggerEnter(Collider other)
    {

        if (invincible) { return; }

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

        if (invincible) { return; }

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


    protected override void UpdateInput()
    {

        Vector2 input = InputManager.instance.GetP2Movement();
        moveInput = new Vector3(input.x, 0, input.y);

        rightInput = InputManager.instance.GetP2RotateRight();
        leftInput = InputManager.instance.GetP2RotateLeft();

        actionInput = InputManager.instance.GetP2Action();

    }

    protected override void UpdateRotation()
    {

        if (rightInput)
        {
            RotateRight();
        }
        else if (leftInput)
        {
            RotateLeft();
        }

        ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.Euler(0, to, 0), Time.deltaTime * rotationSmooth);

    }


    protected override void UpdateAction()
    {


        //If the player presses the action button
        if (actionInput)
        {

            //If the player is in cooldown, just return
            if (cooldownCounter > 0)
            {
                return;
            }

            if (dischargeCounter < dischargeTime)
            {

                //Tried to activate the shield too soon, caused a discharge
                cooldownCounter = dischargeCooldown;

                //No matter what the shield is doing, it will turn off
                shield.Discharge();

                //Revert things
                shielding = false;
                moveSpeed = baseSpeed;

                return;

            }

            //If the player pressed the action button, it will always reset the counter to 0
            dischargeCounter = 0;

            if (!shielding)
            {
                shield.DeployShield(shieldDeploySpeed);
                shielding = true;

                //Slow the player when shield is held
                moveSpeed *= shieldSpeedMult;
            }
            else if (shielding)
            {
                shield.EndShield(shieldDeploySpeed);
                shielding = false;

                //Revert speed
                moveSpeed = baseSpeed;
            }

        }

    }
    //Called in fixed update
    private void UpdateShieldCooldowns ()
    {

        //Discharge counter always will increase
        dischargeCounter += Time.deltaTime;

        //Discharge cooldown will always decrease
        if (cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;
        }

    }



    //UPGRADES
    public void UpgradeShieldSize ()
    {
        shield.UpgradeShield();
    }
    public void UpgradeMovementSpeed ()
    {
        maxSpeed = upgradedMaxSpeed;
        acceleration = upgradedAcceleration;
        deceleration = upgradedDeceleration;
    }
    public void UpgradeDischargeCooldown ()
    {
        dischargeCooldown = upgradedDischargeCooldown;
    }
    public void UpgradeDischargeSize ()
    {
        shield.UpgradeDischarge();
    }


}
