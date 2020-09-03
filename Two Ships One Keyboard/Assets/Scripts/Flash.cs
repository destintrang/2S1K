using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{


    //How long to flash white
    private float flashDuration = 5;

    //How long to flash during death
    private float deathFlashDuration = 10;

    public Material w;
    private Material originalMaterial;
    public MeshRenderer r;


    // Start is called before the first frame update
    void Start()
    {
        originalMaterial = r.material;
    }

    // Update is called once per frame
    void Update()
    {
    }


    //Flashes the material white
    public void PlayFlash ()
    {
        
        if (f != null) { StopCoroutine(f); }
        f = StartCoroutine(FlashColor());

    }

    Coroutine f;
    IEnumerator FlashColor ()
    {

        float counter = 0;
        r.material = w;

        while (counter < flashDuration)
        {
            counter++;
            yield return new WaitForFixedUpdate();
        }

        r.material = originalMaterial;

    }


    public void PlayDeathFlash ()
    {
        StopAllCoroutines();
        StartCoroutine(DeathFlash());
    }
    IEnumerator DeathFlash ()
    {
        float counter = 0;
        r.material = w;
        bool flash = true;

        while (true)
        {
            if (counter >= deathFlashDuration)
            {
                if (flash)
                {
                    r.material = originalMaterial;
                }
                else
                {
                    r.material = w;
                }
                flash = !flash;
                counter = 0;
            }
            else
            {
                counter++;
            }
            yield return new WaitForFixedUpdate();
        }
    }


}
