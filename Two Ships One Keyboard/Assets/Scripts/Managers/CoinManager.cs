using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{



    //Coin to spawn
    [SerializeField] protected GameObject coin;

    //How long coins stay on the level for
    [SerializeField] protected float duration;

    //Object pool
    private List<Coin> pool = new List<Coin>();




    //Singleton
    public static CoinManager instance;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void AddToPool(Coin coin)
    {

        coin.Deactivate();
        pool.Add(coin);

    }
    private Coin PopFromPool()
    {

        Coin c = pool[0];
        pool.Remove(c);

        c.Activate(duration);

        return c;

    }
    public Coin RequestObject ()
    {

        if (pool.Count == 0)
        {
            Coin c = Instantiate(coin, transform).GetComponent<Coin>();
            c.Activate(duration);
            return c;
        }
        else
        {
            return PopFromPool();
        }

    }


}
