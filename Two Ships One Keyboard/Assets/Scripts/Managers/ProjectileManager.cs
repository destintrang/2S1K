using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{


    [SerializeField] protected GameObject projectile;

    private List<Projectile> pool = new List<Projectile>();


    //Singleton
    public static ProjectileManager instance;
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


    public void AddToPool (Projectile p)
    {

        p.Deactivate();
        pool.Add(p);

    }
    private Projectile PopFromPool ()
    {

        Projectile p = pool[0];
        pool.Remove(p);

        p.Activate();

        return p;

    }
    public Projectile RequestProjectile ()
    {

        if (pool.Count == 0)
        {
            return Instantiate(projectile, transform).GetComponent<Projectile>();
        }
        else
        {
            return PopFromPool();
        }

    }


}
