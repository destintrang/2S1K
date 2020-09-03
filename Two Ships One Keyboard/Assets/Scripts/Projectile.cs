using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    

    [SerializeField] protected Color red;
    [SerializeField] protected Color blue;
    [SerializeField] protected Color purple;

    [SerializeField] protected ParticleSystem redTrail;
    [SerializeField] protected ParticleSystem blueTrail;
    [SerializeField] protected ParticleSystem purpleTrail;

    [SerializeField] protected float sparkDuration;
    [SerializeField] protected ParticleSystem sparks;

    private Collision collision;
    private bool reflected = false;

    //These are set when projectiles are fired
    private int damage = 1;
    private Vector3 direction = new Vector3();
    private float speed = 0;
    private GameObject shooter = null;




    private void Awake ()
    {

        //Reference calls
        collision = GetComponent<Collision>();

    }


    private void FixedUpdate()
    {

        transform.Translate(speed * direction * Time.deltaTime);

    }


    public void StartProjectile(Collision.CollisionType color, Vector3 dir, float sp, int dmg, GameObject o = null)
    {

        //Set the owner of this bullet, so that the shooter can't hurt themselves with their own bullet
        if (o != null)
        {
            shooter = o;
        }

        //Just deactivate all trails here to make things easy
        blueTrail.gameObject.SetActive(false);
        redTrail.gameObject.SetActive(false);
        purpleTrail.gameObject.SetActive(false);

        //Reset the reflected state
        reflected = false;

        //Set trajectory, damage, and speed
        damage = dmg;
        direction = dir;
        speed = sp;

        Color actualColor = Color.white;
        if (color == Collision.CollisionType.BLUE) { 
            actualColor = blue;
            blueTrail.gameObject.SetActive(true);
        }
        else if (color == Collision.CollisionType.RED) { 
            actualColor = red;
            redTrail.gameObject.SetActive(true);
        }
        else if (color == Collision.CollisionType.PURPLE) { 
            actualColor = purple;
            purpleTrail.gameObject.SetActive(true);
        }

        collision.SetColor(color, actualColor);

        //Start the projectile duration
        StartCoroutine(ProjectileDuration());

    }
    public int GetDamage ()
    {
        return damage;
    }


    IEnumerator ProjectileDuration ()
    {

        float timer = 0f;

        while (timer < 20f)
        {
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        DestroyProjectile();

    }
    public void DestroyProjectile ()
    {
        ProjectileManager.instance.AddToPool(this);
    }



    //Called when a projectile gets reflected by, say, the blue shield
    public void ReflectProjectile (Vector3 normal)
    {

        //Each projectile can only be reflected once for now
        if (reflected) { return; }
        else
        {
            direction = Vector3.Reflect(direction, normal);
            reflected = true;
        }

    }



    public void OnHit ()
    {

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        FindObjectOfType<SparksManager>().Spark(transform.position, direction);

        //Play on hit SFX
        FindObjectOfType<AudioManager>().Play("On Hit");

        DestroyProjectile();

    }

    public bool IsOwner (GameObject o)
    {
        //Debug.Log(o.name + shooter.name);
        return shooter == o;
    }


    public void Activate ()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate ()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }


}
