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

    private Dictionary<SAMSiteController, GameObject> samTargets = new Dictionary<SAMSiteController, GameObject>();


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
        samTargets.Clear();
        foreach (GameObject missile in missilesOnAir)
        {
            if (missile != null && missile.GetComponent<MissileController>().MissileType == EMissileType.STS)
            {
                foreach (GameObject radar in trackingRadars)
                {
                    if (radar != null)
                    {
                        RadarController radarController = radar.GetComponent<RadarController>();
                        if (Vector2.Distance(radarController.transform.position, missile.transform.position) < radarController.Coverage)
                        {
                            foreach (SAMSiteController sAMSiteController in radarController.ConnectedSAMSites)
                            {
                                if (sAMSiteController != null && !samTargets.ContainsKey(sAMSiteController))
                                {
                                    Debug.DrawLine(sAMSiteController.transform.position, missile.transform.position, Color.red, 500);
                                    samTargets.Add(sAMSiteController, missile);
                                }
                            }
                        }
                    }
                }
            }
        }

        foreach (KeyValuePair<SAMSiteController, GameObject> samTarget in samTargets)
        {
            samTarget.Key.fireMissile(samTarget.Value);
        }
    }

    internal void addItem(GameObject itemToAdd)
    {

        if (itemToAdd.GetComponent<SAMSiteController>() != null)
        {
            samSites.Add(itemToAdd);
            foreach (GameObject radar in trackingRadars)
            {
                radar.GetComponent<RadarController>().connectSamSite(itemToAdd.GetComponent<SAMSiteController>());
            }
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
            Debug.LogError("Couldn't add the item into any list: " + itemToAdd.name);
        }
    }



}
