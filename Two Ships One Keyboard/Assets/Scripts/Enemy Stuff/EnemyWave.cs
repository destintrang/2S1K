using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{


    //The different types of enemies that can spawn in this wave
    [SerializeField] protected List<Enemy> enemyTypes = new List<Enemy>();
    [SerializeField] protected int maxEnemies = 0;


    public List<Enemy> GetEnemyTypes () { return enemyTypes; }
    public int GetMaxEnemies () { return maxEnemies; }

}
