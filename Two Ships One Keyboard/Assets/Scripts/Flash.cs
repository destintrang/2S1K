using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{


    //How long to flash white
    private float flashDuration = 15;

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
            yield return null;
        }

        r.material = originalMaterial;

    }


}
