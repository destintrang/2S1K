using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGroup : MonoBehaviour
{



    //The different types of enemies that can spawn from this group
    public List<Enemy> enemyTypes = new List<Enemy>();
    public int maxEnemies = 0;


    public List<Enemy> GetEnemyTypes() { return enemyTypes; }
    public int GetMaxEnemies() { return maxEnemies; }



}
