﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RadarController : MonoBehaviour, ActionItemActionHandler, Destroyable
{

    [SerializeField]
    private GameObject coverageAreaCircle;

    [SerializeField]
    private float coverage;

    [SerializeField]
    private bool radarOn;

    [SerializeField]
    private List<SAMSiteController> connectedSAMSites = new List<SAMSiteController>();

    [SerializeField]
    private List<GameObject> missileTargets = new List<GameObject>();

    public float Coverage { get => coverage; set => coverage = value; }
    public List<SAMSiteController> ConnectedSAMSites { get => connectedSAMSites; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        coverageAreaCircle.transform.localScale = new Vector3(2 * Coverage, 0.01f, 2 * Coverage);
    }

    private void setOnOff()
    {
        radarOn = !radarOn;
        Debug.Log("Radar is:" + radarOn);
    }

    public void onMouseClick(string actionName)
    {
        Debug.Log("Mouse clicked on Me with Action: " + actionName);
        setOnOff();
    }

    public void destroyMe()
    {
        Destroy(gameObject);
    }

    public bool connectSamSite(SAMSiteController aSAMSiteController)
    {
        if (!ConnectedSAMSites.Contains(aSAMSiteController) && Vector3.Distance(transform.position, aSAMSiteController.transform.position) < Coverage)
        {
            Debug.Log("Connected SAMSite");
            Debug.DrawLine(transform.position, aSAMSiteController.transform.position, Color.red);
            ConnectedSAMSites.Add(aSAMSiteController);
            return true;
        }
        else
        {
            Debug.Log("Not Connected SAMSite");
            return false;
        }

    }

    internal void addMissileTarget(GameObject missile)
    {
        missileTargets.Add(missile);
    }

    internal void lockTargets()
    {
        List<GameObject>.Enumerator missileTargetEnum = missileTargets.GetEnumerator();
        //clone the connected samsites
        Queue<SAMSiteController> samSites = new Queue<SAMSiteController>(connectedSAMSites);
        bool hasNext = missileTargetEnum.MoveNext();
        while (hasNext && samSites.Count > 0)
        {
            GameObject missile = missileTargetEnum.Current;
            SAMSiteController samSite = samSites.Dequeue();
            if (!samSite.Locked)
            {
                //if can't lock (mostly because the missile is not in SAM's range, enqueue for other missiles
                if (!samSite.lockTarget(missile))
                {
                    samSites.Enqueue(samSite);
                }
            }
            hasNext = missileTargetEnum.MoveNext();

            //if there are no more missiles and idle samsites, lock them to the latest missile
            //TODO this is not the smartest resource usage, let's improve it afterwards
            if (!hasNext)
            {
                while (samSites.Count > 0)
                {
                    samSite = samSites.Dequeue();
                    if (!samSite.Locked)
                    {
                        samSite.lockTarget(missile);
                    }
                }
            }
        }

        missileTargets.Clear();
    }
}
