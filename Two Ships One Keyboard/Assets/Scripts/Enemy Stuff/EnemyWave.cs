using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{




    [Serializable]
    public struct Group
    {
        //Max enemies that can be alive at once from this group
        public int max;
        //Total enemies from this group that must be killed
        public int total;
        //Different kinds of enemies that can spawn from this group
        public List<Enemy> enemyList ;
    }

    public List<Group> groups;

    //Counts of active enemies for each group
    private List<int> enemyCounters;
    //Counts of killed enemies for each group
    private List<int> killCounters;
    //Counts of spawned enemies for each group
    private List<int> spawnedCounters;


    //Level bounds
    private float xBound;
    private float zBound;

    //Reference to the location of the player, for spawning
    private List<Transform> players;
    //Make sure players aren't within this range when spawning an enemy
    private float safetyDistance;

    //Delay before spawning the next enemy
    private float spawnCooldown;
    private float spawnTimer;


    public virtual void StartWave (float x, float z, List<Transform> p, float sD, float sC)
    {

        //Initialize the current number of enemies for each group (should all be 0)
        enemyCounters = new List<int>();
        killCounters = new List<int>();
        spawnedCounters = new List<int>();
        foreach (Group g in groups)
        {
            enemyCounters.Add(0);
            killCounters.Add(0);
            spawnedCounters.Add(0);
        }

        xBound = x;
        zBound = z;
        players = p;
        safetyDistance = sD;
        spawnCooldown = sC;
        spawnTimer = 0;

    }
    public virtual void UpdateWave ()
    {

        if (spawnTimer < spawnCooldown)
        {
            spawnTimer += Time.deltaTime;
            return;
        }

        if (SpawnEnemy())
        {
            spawnTimer = 0;
        }
    }

    //Returns true if an enemy was spawned
    private bool SpawnEnemy ()
    {

        for (int i = 0; i < groups.Count; i++)
        {
            if (enemyCounters[i] < groups[i].max && spawnedCounters[i] < groups[i].total)
            {
                //Debug.Log(i);
                enemyCounters[i]++;
                spawnedCounters[i]++;
                Enemy e = GetRandomEnemyType(groups[i]);
                Enemy spawnedEnemy = Instantiate(e, GetSpawnLocation(), Quaternion.identity);
                spawnedEnemy.SetGroup(i);
                return true ;
            }
        }

        return false;

    }

    
    private Enemy GetRandomEnemyType(Group g)
    {

        return g.enemyList[UnityEngine.Random.Range(0, g.enemyList.Count)];
        
    }


    //Function that decrements the number of active enemies, given the index of the group the enemy belonged to
    public void OnEnemyDeath (int groupIndex)
    {

        enemyCounters[groupIndex]--;
        killCounters[groupIndex]++;
        if (CheckWaveCompletion())
        {
            //We finished the wave!
            FindObjectOfType<EnemyWaveManager>().FinishWave();
        }

    }

    //Returns true if the wave is done, false if not
    private bool CheckWaveCompletion ()
    {

        for (int i = 0; i < groups.Count; i++)
        {
            if (killCounters[i] < groups[i].total)
            {
                return false;
            }
        }

        return true;

    }



    //For getting spawn location
    private Vector3 GetSpawnLocation()
    {

        while (true)
        {

            Vector3 randomPosition = GetRandomBorderPosition();
            //Need to check later to make sure it doesn't spawn on the player
            return randomPosition;

        }

    }
    private Vector3 GetRandomBorderPosition()
    {

        //How close to the border enemies spawn
        float borderFraction = 0.65f;
        float randomX = 0;
        float randomZ = 0;

        //50/50 of X/Z
        if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5)
        {
            randomX = UnityEngine.Random.Range(xBound * borderFraction, xBound);
            randomZ = UnityEngine.Random.Range(0, zBound);

            //50/50 of which side to spawn on
            if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5) { randomX = -randomX; }
            if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5) { randomZ = -randomZ; }
        }
        else
        {
            randomX = UnityEngine.Random.Range(0, xBound);
            randomZ = UnityEngine.Random.Range(zBound * borderFraction, zBound);

            //50/50 of which side to spawn on
            if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5) { randomX = -randomX; }
            if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5) { randomZ = -randomZ; }
        }

        return new Vector3(randomX, 0, randomZ);

    }
    private bool IsOnLevel(Vector3 position)
    {

        if (position.x <= xBound && position.x >= -xBound &&
            position.z <= zBound && position.z >= -zBound)
        {
            return true;
        }

        return false;

    }



}
