using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{


    //Speed of the objects
    [SerializeField] protected float objectSpeed;

    //Average delay between spawns
    [SerializeField] protected float delay;
    private float delayCounter;

    //Size randomizer
    [SerializeField] protected float sizeRandomizer;
    private float originalSize;

    //Object pool
    [SerializeField] protected GameObject obj;
    private List<GameObject> pool = new List<GameObject>();
    //List of objects active in the scene
    private List<GameObject> activeObjects = new List<GameObject>();

    //Reference to the level, so we can find where to spawn
    [SerializeField] protected GameObject level;
    private float xBound;
    private float zBound;


    // Start is called before the first frame update
    void Start()
    {

        //Spawning boundaries of where objects can be spawned
        SetSpawnBounds();
        //Set delay
        delayCounter = Random.Range(0, delay * 2);


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (delayCounter >= 0)
        {

            delayCounter--;
            return;

        }
        else
        {

            RequestObject();
            delayCounter = Random.Range(0, delay * 2);

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (activeObjects.Contains(other.gameObject))
        {
            activeObjects.Remove(other.gameObject);
            AddToPool(other.gameObject);
        }
    }


    public void AddToPool(GameObject p)
    {

        p.SetActive(false);
        pool.Add(p);

    }
    private GameObject PopFromPool()
    {

        GameObject p = pool[0];
        pool.Remove(p);

        p.transform.position = GetRandomLocation();

        return p;

    }
    private GameObject RequestObject()
    {

        if (pool.Count == 0)
        {
            GameObject o = Instantiate(obj, GetRandomLocation(), Quaternion.identity, transform);
            o.GetComponent<GoStraightDown>().SetSpeed(objectSpeed);
            RandomizeSize(o);
            activeObjects.Add(o);
            return o;
        }
        else
        {
            GameObject o = PopFromPool();
            RandomizeSize(o);
            o.SetActive(true);
            activeObjects.Add(o);
            return o;
        }

    }

    private Vector3 GetRandomLocation ()
    {
        return new Vector3(Random.Range(-xBound, xBound), transform.position.y, transform.position.z);
    }
    private void RandomizeSize (GameObject o)
    {

        o.transform.localScale = obj.transform.localScale;
        float scale = 1 + Random.Range(-sizeRandomizer, sizeRandomizer);
        o.transform.localScale *= scale;

    }


    //Set the spawning boundaries, based on the size of the level
    private void SetSpawnBounds()
    {

        //Minus 1 to keep things from spawning on the very edge of the level
        xBound = (level.transform.localScale.x / 2) - 1;
        zBound = (level.transform.localScale.z / 2) - 1;

    }
}
