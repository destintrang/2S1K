using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyWaveManager : MonoBehaviour
{


    [SerializeField] protected List<EnemyWave> waves;
    EnemyWave currentWave;
    private int waveCounter = -1;

    //How long breaks are in seconds
    [SerializeField] protected float breakTime;

    //Bool of whether we can spawn right now
    private bool canSpawn = true;

    //Reference to the level, so we can find where to spawn
    [SerializeField] protected GameObject level;
    private float xBound;
    private float zBound;

    //Current number of active enemies
    private int activeEnemies;

    //Reference to the location of the player, for spawning
    [SerializeField] protected List<Transform> players;
    //Make sure players aren't within this range when spawning an enemy
    [SerializeField] protected float safetyDistance;

    //Delay before spawning the next enemy
    [SerializeField] protected float spawnCooldown;
    private float spawnTimer;


    //Singleton
    public static EnemyWaveManager instance;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

        //Spawning boundaries of where enemies can be spawned
        SetSpawnBounds();
        //For now, just increment here
        IncrementWave();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateWave();
    }



    //Call this when any enemy dies
    public void OnEnemyDeath (int g)
    {
        currentWave.OnEnemyDeath(g);
    }
    //Call this when the boss dies
    public void OnBossDeath ()
    {
        FinishWave();
    }



    private void UpdateWave ()
    {

        if (!canSpawn) { return; }
        currentWave.UpdateWave();
        return;


    }



    public void IncrementWave ()
    {

        if (currentWave != null)
        {
            Destroy(currentWave.gameObject);
            currentWave = null;
        }

        waveCounter++;

        //We won!
        if (waveCounter == waves.Count)
        {
            FindObjectOfType<GameManager>().WinGame();
            return;
        }

        currentWave = Instantiate(waves[waveCounter], this.transform);
        currentWave.StartWave(xBound, zBound, players, safetyDistance, spawnCooldown);

    }
    public void FinishWave ()
    {
        StartCoroutine(Break());
    }
    IEnumerator Break ()
    {
        yield return new WaitForSeconds(breakTime);
        IncrementWave();
    }



    private Vector3 GetSpawnLocation ()
    {

        while (true)
        {

            Vector3 randomPosition = GetRandomBorderPosition();
            //Need to check later to make sure it doesn't spawn on the player
            return randomPosition;

        }
        
    }
    private Vector3 GetRandomBorderPosition ()
    {

        //How close to the border enemies spawn
        float borderFraction = 0.65f;
        float randomX = 0;
        float randomZ = 0;

        //50/50 of X/Z
        if (Random.Range(0.0f, 1.0f) > 0.5)
        {
            randomX = Random.Range(xBound * borderFraction, xBound);
            randomZ = Random.Range(0, zBound);

            //50/50 of which side to spawn on
            if (Random.Range(0.0f, 1.0f) > 0.5) { randomX = -randomX; }
            if (Random.Range(0.0f, 1.0f) > 0.5) { randomZ = -randomZ; }
        }
        else
        {
            randomX = Random.Range(0, xBound);
            randomZ = Random.Range(zBound * borderFraction, zBound);

            //50/50 of which side to spawn on
            if (Random.Range(0.0f, 1.0f) > 0.5) { randomX = -randomX; }
            if (Random.Range(0.0f, 1.0f) > 0.5) { randomZ = -randomZ; }
        }

        return new Vector3(randomX, 0, randomZ);

    }
    private bool IsOnLevel (Vector3 position)
    {

        if (position.x <= xBound && position.x >= -xBound &&
            position.z <= zBound && position.z >= -zBound)
        {
            return true;
        }

        return false;

    }


    


    public int GetClearedWaves()
    {
        int cleared = waveCounter;
        return cleared;
    }
    public int GetCurrentWave()
    {
        int cleared = waveCounter + 1;
        return cleared;
    }


    public void ToggleSpawning () { canSpawn = !canSpawn; }


    //Set the spawning boundaries, based on the size of the level
    private void SetSpawnBounds ()
    {

        //Minus 1 to keep things from spawning on the very edge of the level
        xBound = (level.transform.localScale.x / 2) - 1;
        zBound = (level.transform.localScale.z / 2) - 1;

    }

}
