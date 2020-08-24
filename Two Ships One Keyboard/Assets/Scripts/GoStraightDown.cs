using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoStraightDown : MonoBehaviour
{


    [SerializeField] protected float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Vector3.back * Time.deltaTime;
    }


    public void SetSpeed (float s) { speed = s; }

}
