using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().Play("Drill Spin");
    }

    // Update is called once per frame
    void Update()
    {


    }
}
