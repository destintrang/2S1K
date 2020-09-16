using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMagnet : Magnet
{

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateMagnet();
    }


    protected override void UpdateMagnet ()
    {

        bool red = false;
        bool blue = false;

        BasePlayerShip r = FindObjectOfType<AttackShip>();
        BasePlayerShip b = FindObjectOfType<DefenseShip>();

        if (Vector3.Distance(r.transform.position, transform.position) <= r.GetMagnetRange() && !r.IsBarrierActive()) { red = true; }
        if (Vector3.Distance(b.transform.position, transform.position) <= b.GetMagnetRange() && !b.IsBarrierActive()) { blue = true; }

        if (r.IsBarrierActive()) { red = false; }
        if (b.IsBarrierActive()) { blue = false; }

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


}
