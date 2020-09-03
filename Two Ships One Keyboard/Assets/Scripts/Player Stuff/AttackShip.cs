using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : BasePlayerShip
{



    //Cooldown after firing
    [SerializeField] protected float fireCooldown;
    [SerializeField] protected float upgradedFireCooldown;
    private float fireCounter = 0;

    //Speed this ship's projectiles are fired at
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float upgradedProjectileSpeed;

    //How much damage these bullets do
    private int damage = 1;

    //Gun positions we'll shoot from
    [SerializeField] protected List<Transform> guns;
    private int gunCounter = 0;
    private bool gunsUpgraded = false;



    // Update is called once per frame
    void Update()
    {

        UpdateInput();
        UpdateRotation();

    }

    private void FixedUpdate()
    {

        //Handle actually moving the ship here
        MoveShip();
        UpdateAction();


    }



    //Red ship will die to blue bullets
    protected override void OnTriggerEnter(Collider other)
    {

        if (invincible) { return; }

        if (other.GetComponent<Collision>() != null && other.GetComponent<Collision>().GetColor() != Collision.CollisionType.RED)
        {

            //Red ship got hit
            TakeDamage();

        }

    }
    //Red ship will die to blue bodies
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {

        if (invincible) { return; }

        if (collision.gameObject.GetComponentInChildren<Collision>() != null && collision.gameObject.GetComponentInChildren<Collision>().GetColor() != Collision.CollisionType.RED)
        {

            //Red ship got hit
            TakeDamage();

        }

    }
    protected override void TakeDamage()
    {
        GameManager.instance.LoseGame();
    }
    //Red ship dies to the blue shockwave
    public override void OnShockwave()
    {

        TakeDamage();

    }


    protected override void UpdateInput()
    {

        Vector2 input = InputManager.instance.GetP1Movement();
        moveInput = new Vector3(input.x, 0, input.y);

        rightInput = InputManager.instance.GetP1RotateRight();
        leftInput = InputManager.instance.GetP1RotateLeft();

        actionInput = InputManager.instance.GetP1Action();

    }

    protected override void UpdateRotation()
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


    protected override void UpdateAction()
    {

        fireCounter -= Time.deltaTime;

        if (actionInput && fireCounter <= 0)
        {

            Fire();

        }

    }

    void Fire ()
    {

        if (!gunsUpgraded)
        {
            Projectile p = ProjectileManager.instance.RequestProjectile();

            p.transform.position = guns[gunCounter].position;
            gunCounter = (gunCounter + 1) % guns.Count;

            p.StartProjectile(Collision.CollisionType.RED, ship.forward, projectileSpeed, damage);
        }
        else
        {

            Projectile p1 = ProjectileManager.instance.RequestProjectile();
            Projectile p2 = ProjectileManager.instance.RequestProjectile();

            p1.transform.position = guns[0].position;
            p2.transform.position = guns[1].position;

            p1.StartProjectile(Collision.CollisionType.RED, ship.forward, projectileSpeed, damage);
            p2.StartProjectile(Collision.CollisionType.RED, ship.forward, projectileSpeed, damage);

        }
        fireCounter = fireCooldown;

        //Play the sound effect
        FindObjectOfType<AudioManager>().Play("Red Ship Fire");

    }




    //UDGRADES
    public void UpgradeDamage()
    {

    }
    public void UpgradeGuns()
    {
        gunsUpgraded = true;
    }
    public void UpgradeAttackSpeed ()
    {
        fireCooldown = upgradedFireCooldown;
    }
    public void UpgradeProjectileSpeed ()
    {
        projectileSpeed = upgradedProjectileSpeed;
    }
    public void UpgradeMovementSpeed ()
    {
        maxSpeed = upgradedMaxSpeed;
        acceleration = upgradedAcceleration;
        deceleration = upgradedDeceleration;
    }


}
