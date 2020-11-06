using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{

    [SerializeField] protected float x;
    [SerializeField] protected float y;
    [SerializeField] protected float z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x + x, transform.localRotation.eulerAngles.y + y, transform.localRotation.eulerAngles.z + z);
        //transform.localRotation.eulerAngles.Set(transform.localRotation.eulerAngles.x + x, transform.localRotation.eulerAngles.y + y, transform.localRotation.eulerAngles.z + z);

    }
}
