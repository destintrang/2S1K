using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    
    //Should this enemy target the red, blue, or both ships?
    public enum TargetPlayer { RED, BLUE, BOTH };
    [SerializeField] protected TargetPlayer target;

    public TargetPlayer GetTarget () { return target; }

}
