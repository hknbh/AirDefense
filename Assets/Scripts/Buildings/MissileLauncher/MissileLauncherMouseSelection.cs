using System;
using System.Collections;
using UnityEngine;



public class MissileLauncherMouseSelection : MouseSelectionScript
{
    public void Start()
    {
        base.Start();
        TargetAction = shootMissile;
    }


    private void shootMissile(GameObject target)
    {
        if (target.tag.Equals("Terrain"))
        {
            Debug.Log("Clicked on Terrain, skipping missile launch");
        }
        else
        {
            Debug.Log("Shoot missile to:" + target.transform.position);
            GetComponent<MissileLauncherController>().fireMissile(target);
        }
        
    }
}
