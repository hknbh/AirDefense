using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCenterController : MonoBehaviour
{
    private List<GameObject> trackingRadars = new List<GameObject>();
    private List<GameObject> samSites = new List<GameObject>();
    private List<GameObject> missileLaunchers = new List<GameObject>();
    private List<GameObject> missilesOnAir = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        clearNulls();
        checkMissilesOnAir();
    }

    private void clearNulls()
    {
        samSites.RemoveAll(item => item == null);
        missilesOnAir.RemoveAll(item => item == null);
        trackingRadars.RemoveAll(item => item == null);
        missileLaunchers.RemoveAll(item => item == null);

    }

    private void checkMissilesOnAir()
    {
 
        foreach (GameObject missile in missilesOnAir)
        {
            if (missile != null)
            {
                foreach (GameObject samSite in samSites)
                {
                    if (samSite != null)
                    {
                        SAMSiteController sAMSiteController = samSite.GetComponent<SAMSiteController>();
                        if (sAMSiteController != null)
                        {
                            sAMSiteController.fireMissile(missile);
                        }
                    }
                   
                }
            }
        }
    }

    internal void addItem(GameObject itemToAdd)
    {
        Debug.Log("Add item: " + itemToAdd.name);
        if (itemToAdd.GetComponent<SAMSiteController>() != null)
        {
            samSites.Add(itemToAdd);
        }
        else if (itemToAdd.GetComponent<MissileLauncherController>() != null)
        {
            missileLaunchers.Add(itemToAdd);
        }
        else if (itemToAdd.GetComponent<RadarController>() != null)
        {
            trackingRadars.Add(itemToAdd);
        }
        else if (itemToAdd.GetComponent<MissileController>() != null)
        {
            missilesOnAir.Add(itemToAdd);
        }
        else
        {
            Debug.LogError("Couldn't add the item into any list");
        }
    }
}
