using System;
using System.Collections;
using UnityEngine;



public class RadarMouseSelection : MouseSelectionScript
{
    void Start()
    {
        Debug.Log("S T A R T");
        TargetAction = shootMissile;
    }


    private void shootMissile(GameObject target)
    {
        Debug.Log("Shoot missile to:" + target.transform.position);
        GetComponent<RadarScript>().fireMissile(target);
    }
}
