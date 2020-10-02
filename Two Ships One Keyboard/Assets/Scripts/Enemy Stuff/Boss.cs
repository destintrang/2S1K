using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    //After the boss takes damage, also needs to update the boss health bar
    public override void TakeDamage(int damage)
    {

        base.TakeDamage(damage);
        FindObjectOfType<HUDManager>().UpdateBossHealthBar(GetHealthPercentage());

    }

    public override void KillEnemy()
    {
        //Alert the wave manager that the boss has died
        EnemyWaveManager.instance.OnBossDeath();

        //Finally destroy the enemy object
        Destroy(this.gameObject);

    }

    //Bosses take damage instead of dying instantly
    public override void OnShockwave()
    {
        TakeDamage(5);
    }

}
