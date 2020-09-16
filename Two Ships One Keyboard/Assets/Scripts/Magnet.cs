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
    void FixedUpdate()
    {
        UpdateMagnet();
    }


    protected virtual void UpdateMagnet ()
    {

        bool red = false;
        bool blue = false;

        BasePlayerShip r = FindObjectOfType<AttackShip>();
        BasePlayerShip b = FindObjectOfType<DefenseShip>();

        if (Vector3.Distance(r.transform.position, transform.position) <= r.GetMagnetRange()) { red = true; }
        if (Vector3.Distance(b.transform.position, transform.position) <= b.GetMagnetRange()) { blue = true; }

        if (red && blue)
        {

            Vector3 playerPos = FindClosestPlayer();
            
            UpdatePosition(playerPos, 1);

        }
        else if (red)
        {
            UpdatePosition(r.transform.position, 1);
        }
        else if (blue)
        {
            UpdatePosition(b.transform.position, 1);
        }

    }
    protected void UpdatePosition (Vector3 playerPos, float strength)
    {
        transform.position = Vector3.Slerp(transform.position, playerPos, magnetStrength * Time.deltaTime * strength);
    }


    //Can only magnetize to one player, so magnetize to the closest one
    protected Vector3 FindClosestPlayer ()
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
