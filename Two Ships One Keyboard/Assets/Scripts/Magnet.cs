using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{


    [SerializeField] protected float magnetRange;
    [SerializeField] protected float magnetStrength;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMagnet();
    }


    void UpdateMagnet ()
    {

        Vector3 playerPos = FindClosestPlayer();
        float distance = Vector3.Distance(playerPos, transform.position);

        //Check if the player is close enough to this object
        if (distance <= magnetRange)
        {
            UpdatePosition(playerPos, magnetRange - distance);
        }

    }
    void UpdatePosition (Vector3 playerPos, float strength)
    {
        transform.position = Vector3.Slerp(transform.position, playerPos, magnetStrength * Time.deltaTime * strength);
    }


    //Can only magnetize to one player, so magnetize to the closest one
    Vector3 FindClosestPlayer ()
    {

        Vector3 attackPos = FindObjectOfType<AttackShip>().transform.position;
        float attackDistance = Vector3.Distance(attackPos, transform.position);

        Vector3 defensePos = FindObjectOfType<DefenseShip>().transform.position;
        float defenseDistance = Vector3.Distance(defensePos, transform.position);

        if (attackDistance < defenseDistance) { return attackPos; }
        else if (attackDistance >= defenseDistance) { return defensePos; }
        else { return Vector3.zero; }

    }


}
