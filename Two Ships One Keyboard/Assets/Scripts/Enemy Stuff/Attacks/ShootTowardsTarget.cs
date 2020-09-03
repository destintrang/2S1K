using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTowardsTarget : EnemyAttack
{



    //Who we want to shoot at
    protected Targeting.TargetPlayer target;

    [SerializeField] protected Collision.CollisionType projectileType;

    //How fast fired projectiles will fly at
    [SerializeField] protected float projectileSpeed;

    //Degrees that the shot can be inaccurate by
    [SerializeField] protected float inaccuracy;

    //Time between firing projectiles
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackCooldownRandomizer;
    protected float cooldownCounter = 0;

    //Where to spawn the bullets from
    [SerializeField] protected Transform ship;



    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Targeting>().GetTarget();
        //Delay first attack
        cooldownCounter = attackCooldown;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //If not actionable, don't do anything
        if (!GetComponent<Enemy>().IsActionable()) { return; }

        UpdateAttack();

    }


    protected virtual void UpdateAttack ()
    {

        //Update cooldown, and if still on cooldown, don't attack
        cooldownCounter -= 1;
        if (cooldownCounter > 0) { return; }

        //We can fire
        ShootTarget(GetTargetLocation(target));
        cooldownCounter = DelayRandomizer(attackCooldown, attackCooldownRandomizer);

    }


    protected void ShootTarget (Vector3 target)
    {
        //Get a projectile and place it at the enemy's position
        Projectile p = ProjectileManager.instance.RequestProjectile();
        p.transform.position = ship.position + new Vector3(0, 1, 0);

        //Calculate direction to send the projectile in
        Vector3 playerPos = target;
        Vector3 dir = (playerPos - transform.position).normalized;
        dir = AccuracyRandomizer(dir, inaccuracy);

        p.StartProjectile(projectileType, dir, projectileSpeed, 1, this.gameObject);
    }



}
