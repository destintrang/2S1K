using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    
    public enum CollisionType { RED, BLUE, PURPLE };
    [SerializeField] protected CollisionType color;

    [SerializeField] protected Material redMaterial;
    [SerializeField] protected Material blueMaterial;
    [SerializeField] protected Material purpleMaterial;


    public CollisionType GetColor() { return color; }
    public void SetColor(Collision.CollisionType c, Color actualColor) {

        color = c;
        GetComponentInChildren<MeshRenderer>().material.color = actualColor;

        //The only time this is called is when bullets are being re-assigned
        //So lets also set glow as well here
        if (color == CollisionType.RED)
        {
            GetComponentInChildren<MeshRenderer>().material = redMaterial;
        }
        else if (color == CollisionType.BLUE)
        {
            GetComponentInChildren<MeshRenderer>().material = blueMaterial;
        }
        else //Purple
        {
            GetComponentInChildren<MeshRenderer>().material = purpleMaterial;
        }

    }


}
