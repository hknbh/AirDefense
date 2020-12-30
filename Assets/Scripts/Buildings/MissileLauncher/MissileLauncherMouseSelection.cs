using System;
using System.Collections;
using UnityEngine;



public class MissileLauncherMouseSelection : MouseSelectionScript
{
    public void Start()
    {
        base.Start();
        TargetAction = lockTarget;
    }


    private void lockTarget(GameObject target)
    {
        if (target.tag.Equals("Terrain"))
        {
            Debug.Log("Clicked on Terrain, skipping missile launch");
        }
        else
        {
            GetComponent<MissileLauncherController>().lockTarget(target);
        }
        
    }
}
