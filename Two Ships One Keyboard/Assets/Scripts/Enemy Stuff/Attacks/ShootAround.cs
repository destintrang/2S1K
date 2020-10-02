using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAround : ShootTowardsTarget
{


    [SerializeField] protected int projectileNum;


    // Start is called before the first frame update
    void Start()
    {

        //Delay first attack
        cooldownCounter = attackCooldown;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!alwaysAttack) { return; }

        //If not actionable, don't do anything
        if (!GetComponent<Enemy>().IsActionable()) { return; }

        UpdateAttack();

    }


    protected override void UpdateAttack()
    {

        //Update cooldown, and if still on cooldown, don't attack
        cooldownCounter -= 1;
        if (cooldownCounter > 0) { return; }

        ShootAroundEnemy(projectileType);

        cooldownCounter = DelayRandomizer(attackCooldown, attackCooldownRandomizer);

    }

    public void ShootAroundEnemy (Collision.CollisionType type)
    {

        //Fire multiple shots!
        float degrees = 360 / projectileNum;
        for (int i = 0; i < projectileNum; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * (transform.position + Vector3.forward);
            Vector3 f = dir - transform.position;
            dir = Quaternion.AngleAxis(degrees * i, Vector3.up) * f;
            ShootTarget(dir + transform.position, type);
        }
        //ShootTarget(transform.position + Vector3.forward);

    }


}
