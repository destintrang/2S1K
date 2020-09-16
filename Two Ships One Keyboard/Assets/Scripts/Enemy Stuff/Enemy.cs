using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseShip
{


    public int maxHealth;
    private int health;

    //Is this enemy at 0 health yet?
    private bool dead = false;

    [SerializeField] protected int points;
    [SerializeField] protected int coins;

    //Reference to the movement script of this enemy
    EnemyMovement movement;


    private Transform target;

    //Whether the enemy can move/attack; starts off, turned on after spawn animation
    private bool actionable = false;
    public bool IsActionable () { return actionable; }
    public void Activate () { actionable = true; }

    //What color enemy this is
    private Collision collisionType;


    //For notifying which group this enemy belonged to
    private int groupIndex;
    public void SetGroup (int g) { groupIndex = g; }



    // Start is called before the first frame update
    void Start()
    {

        health = maxHealth;

        //Reference calls
        collisionType = GetComponent<Collision>();
        movement = GetComponent<EnemyMovement>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    protected override void OnTriggerEnter(Collider other)
    {

        //Only handle collisions from projectiles
        Projectile p = other.GetComponent<Projectile>();
        if (p == null) { return; }

        if (other.GetComponent<Collision>() != null && other.GetComponent<Collision>().GetColor() != collisionType.GetColor())
        {

            if (p.IsOwner(this.gameObject))
            {
                return;
            }

            //Enemy got hit by a different colored bullet
            TakeDamage(p.GetDamage());
            p.OnHit();

        }

    }


    //Called when any enemy dies; play death animation
    private void OnZeroHealth ()
    {

        //Need to increment score/EXP

        //Spawn coins
        SpawnCoins();
        //Check to see if we spawn a barrier pickup
        FindObjectOfType<BarrierPickupManager>().SpawnBarrier(transform.position);
        //Increment points
        FindObjectOfType<ScoreManager>().IncreaseScore(points);

        //Disable action
        actionable = false;

        //Disable the enemy's collider
        GetComponent<CapsuleCollider>().enabled = false;

        //Update that enemy is dead
        dead = true;

        //Finally, play the death animation
        GetComponent<Animator>().Play("Enemy Death");
        //Start flashing
        GetComponent<Flash>().PlayDeathFlash();
        //explode
        FindObjectOfType<ExplosionManager>().Explode(transform.position);
        //Explosion SFX
        FindObjectOfType<AudioManager>().Play("Death Explosion");

    }
    //Called after the enemy's death animation
    public void KillEnemy ()
    {

        //Alert the wave manager that an enemy has died
        EnemyWaveManager.instance.OnEnemyDeath(groupIndex);

        //Finally destroy the enemy object
        Destroy(this.gameObject);

    }


    //Call this when hit by the player projectile
    public virtual void TakeDamage (int damage)
    {

        //The enemy doesn't take damage after its dead (zero health)
        if (dead) { return; }

        health -= damage;

        //For now, if the enemy dies, just delete the object
        if (IsDead())
        {
            OnZeroHealth();
        }

        GetComponent<Flash>().PlayFlash();
        float percent = (float) health / maxHealth;

    }
    
    //Called when the enemy is hit by the blue ship's shockwave
    public override void OnShockwave()
    {

        //The enemy doesn't take damage after its dead (zero health)
        if (dead) { return; }

        health = 0;
        OnZeroHealth();

    }



    //Is this enemy dead?
    public bool IsDead ()
    {
        if (health <= 0) { return true; }
        else return false;
    }


    private void SpawnCoins()
    {

        //Spawns number randomly from 1x - 1.5x the specified amount
        int coinsToSpawn = Random.Range( coins, (int)(coins * 1.5));
        const float spawnDistance = 3;

        for (int i = 0; i < coinsToSpawn; i++)
        {

            //Calculate a random position to spawn a coin
            Vector3 randomPos = Random.insideUnitSphere * spawnDistance;
            randomPos = transform.position + new Vector3(randomPos.x, 0, randomPos.z);

            //Spawn the coin
            Coin c = CoinManager.instance.RequestObject();
            c.transform.position = randomPos;

        }

    }


}
