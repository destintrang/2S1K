using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPickup : MonoBehaviour
{


    //Model of the coin
    [SerializeField] protected MeshRenderer mesh;

    [SerializeField] protected float blinkInterval;

    //For randomizing the speed of the coin
    [SerializeField] protected float animationMultiplier;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter(Collider other)
    {


        //Can only be collected by the player ships
        BasePlayerShip p = other.GetComponent<BasePlayerShip>();
        if (p == null) { return; }

        //If the player already has a barrier, they can't pick this up
        if (p.IsBarrierActive()) { return; }

        OnCollect(p);


    }

    //Called when the player touches the coin
    private void OnCollect(BasePlayerShip p)
    {

        GetComponent<Magnet>().enabled = false;

        GetComponent<BoxCollider>().enabled = false;

        //Stop all coroutines: the duration coroutine
        StopAllCoroutines();

        GetComponent<Animator>().Play("Coin Collect");
        p.ActivateBarrier();
        //FindObjectOfType<ScoreManager>().IncreaseCoin();

    }

    //Called after the coin's collect animation has finished
    public void RemoveCoin()
    {

        //CoinManager.instance.AddToPool(this);

    }



    public void Activate(float time)
    {

        //Make sure the mesh is back on
        mesh.enabled = true;

        GetComponent<Magnet>().enabled = true;
        gameObject.SetActive(true);
        StartCoroutine(Duration(time));

    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }


    public void RandomizeAnimationSpeed()
    {
        animator.speed = Random.Range(-animationMultiplier, animationMultiplier) + 1;
    }
    public void ResetAnimationSpeed()
    {
        animator.speed = 1;
    }


    public IEnumerator Duration(float time)
    {

        float counter = 0;
        float blinkCounter = 0;

        while (counter < time)
        {
            counter++;

            //Start to have the mesh blink
            if (counter > time * 0.6f)
            {

                blinkCounter++;
                if (blinkCounter >= blinkInterval)
                {
                    mesh.enabled = !mesh.enabled;
                    blinkCounter = 0;
                }

            }

            yield return new WaitForFixedUpdate();
        }

        Destroy(this.gameObject);
        //FindObjectOfType<CoinManager>().AddToPool(this);

    }


}
