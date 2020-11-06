using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedFollow : MonoBehaviour
{


    //Gameobject to follow
    [SerializeField] protected Transform toFollow;

    private List<Vector3> followPositions = new List<Vector3>();

    //Frames delayed before following starts
    [SerializeField] protected int delay = 60;
    private int counter = 0;

    private Vector3 positionOffset;


    // Start is called before the first frame update
    void Start()
    {
        positionOffset = transform.localPosition;
        transform.position = toFollow.transform.position + positionOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        followPositions.Add(toFollow.position);

        if (delay > 0)
        {
            delay--;
        }
        else
        {
            transform.LookAt(followPositions[counter] + positionOffset);
            transform.position = followPositions[counter] + positionOffset;
            counter++;
        }

    }


    //This object will be a child of the object to follow in the prefab,
    //but when the object starts, this object should be unparented for easy-following
    public void Unparent ()
    {
        transform.parent = null;
    }


}
