using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballMove : MonoBehaviour
{


    [SerializeField] protected float lookDuration;
    private float lookCounter = 0;
    [SerializeField] protected float eyeRadius;

    private Vector3 originalPosition;
    private Transform eyeball;


    // Start is called before the first frame update
    void Start()
    {
        eyeball = transform;
        originalPosition = eyeball.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (lookCounter == 0)
        {
            StopAllCoroutines();
            StartCoroutine(LookAt());
            lookCounter = lookDuration;
        }
        else
        {
            lookCounter--;
        }

    }


    IEnumerator LookAt ()
    {

        Vector2 dir = Random.insideUnitCircle.normalized * eyeRadius;
        Vector3 newPos = originalPosition + new Vector3(dir.x, 0, dir.y);
        Vector3 oldPos = transform.localPosition;
        float counter = 0;
        float duration = lookDuration * 0.4f;

        while (counter < duration)
        {
            transform.localPosition = Vector3.Lerp(oldPos, newPos, counter / duration);

            counter++;
            yield return new WaitForFixedUpdate();
        }

    }


}
