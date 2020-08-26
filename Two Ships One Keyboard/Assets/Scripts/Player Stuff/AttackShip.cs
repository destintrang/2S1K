using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : BasePlayerShip
{



    //Cooldown after firing
    [SerializeField] protected float fireCooldown;
    private float fireCounter = 0;

    //Speed this ship's projectiles are fired at
    [SerializeField] protected float projectileSpeed;

    //How much damage these bullets do
    private int damage = 1;

    //Gun positions we'll shoot from
    [SerializeField] protected List<Transform> guns;
    private int gunCounter = 0;



    // Update is called once per frame
    void Update()
    {

        UpdateMovement();
        UpdateRotation();
        UpdateAction();
        
    }




    //Red ship will die to blue bullets
    protected override void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collision>() != null && other.GetComponent<Collision>().GetColor() != Collision.CollisionType.RED)
        {

            //Red ship got hit
            TakeDamage();

        }

    }
    //Red ship will die to blue bodies
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {

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
    public override void OnShockwave()
    {

        TakeDamage();

    }


    protected override void UpdateMovement()
    {

        Vector2 input = InputManager.instance.GetP1Movement();
        moveInput = new Vector3(input.x, 0, input.y);

        //rb.velocity = direction * moveSpeed;

    }




    protected override void UpdateAction()
    {

        fireCounter -= Time.deltaTime;

        if (InputManager.instance.GetP1Action() && fireCounter <= 0)
        {

            Fire();

        }

    }

    void Fire ()
    {

        Projectile p = ProjectileManager.instance.RequestProjectile();

        p.transform.position = guns[gunCounter].position;
        gunCounter = (gunCounter + 1) % guns.Count; 

        p.StartProjectile(Collision.CollisionType.RED, ship.forward, projectileSpeed, damage);
        fireCounter = fireCooldown;

    }




    //UDGRADES
    public void UpgradeDamage ()
    {

    }
    public void UpgradeAttackSpeed ()
    {

    }
    public void UpgradeProjectileSpeed ()
    {

    }
    public void UpgradeMovementSpeed ()
    {

    }


}
