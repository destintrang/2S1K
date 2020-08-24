using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectPool : MonoBehaviour
{


    //Object to spawn and pool
    [SerializeField] protected GameObject obj;
    //Object pool
    protected Queue<GameObject> pool = new Queue<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddToPool(GameObject o)
    {

        Deactivate(o);
        pool.Enqueue(o);

    }
    private GameObject PopFromPool()
    {

        GameObject o = pool.Dequeue();
        Activate(o);

        return o;

    }
    public GameObject RequestObject(Vector3 location)
    {

        if (pool.Count == 0)
        {
            //Make the pool a child of whatever object this script is attached to
            GameObject o = Instantiate(obj, transform);
            //Place the object at the requested location
            o.transform.position = location;
            return o;
        }
        else
        {
            GameObject o = PopFromPool();
            //Place the object at the requested location
            o.transform.position = location;
            return o;
        }

    }


    //Called on an object that we pull from the pool, which was last deactivated
    protected virtual void Activate(GameObject o)
    {
        o.SetActive(true);
    }
    //Called on an object before we put it into the pool, so it won't bother us
    protected virtual void Deactivate(GameObject o)
    {
        o.SetActive(false);
    }


}
