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
        StartCoroutine("checkAll");
    }


    private IEnumerator checkAll()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(2);
            clearNulls();
            checkMissilesOnAir();
        }
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
            if (missile != null && missile.GetComponent<MissileController>().MissileType == EMissileType.STS)
            {
                foreach (GameObject radar in trackingRadars)
                {
                    if (radar != null)
                    {
                        RadarController radarController = radar.GetComponent<RadarController>();
                        MissileController missileController = missile.GetComponent<MissileController>();
                        float distance = VectorUtils.Vector2Distance(radarController.transform.position, missileController.getMissileBody().transform.position);
                        if (distance < radarController.Coverage)
                        {
                            radarController.addMissileTarget(missile);
                        }
                        radarController.lockTargets();
                    }
                }
            }
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
            RadarController radarController = itemToAdd.GetComponent<RadarController>();
            foreach (GameObject samSite in samSites)
            {
                radarController.connectSamSite(samSite.GetComponent<SAMSiteController>());
            }
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


    internal void removeItem(GameObject itemToRemove)
    {
        if (itemToRemove.GetComponent<SAMSiteController>() != null)
        {
            samSites.Remove(itemToRemove);
            foreach (GameObject radar in trackingRadars)
            {
                radar.GetComponent<RadarController>().connectSamSite(itemToRemove.GetComponent<SAMSiteController>());
            }
        }
        else if (itemToRemove.GetComponent<MissileLauncherController>() != null)
        {
            missileLaunchers.Remove(itemToRemove);
        }
        else if (itemToRemove.GetComponent<RadarController>() != null)
        {
            trackingRadars.Remove(itemToRemove);
        }
        else if (itemToRemove.GetComponent<MissileController>() != null)
        {
            missilesOnAir.Remove(itemToRemove);
        }
        else
        {
            Debug.LogError("Couldn't remove the item:" + itemToRemove.name);
        }
    }

}
