using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RadarController : BuildingController, ActionItemActionHandler
{
    [SerializeField]
    private GameObject coverageAreaCircle;

    [SerializeField]
    private float coverage;

    [SerializeField]
    private List<SAMSiteController> connectedSAMSites = new List<SAMSiteController>();

    [SerializeField]
    private List<GameObject> missileTargets = new List<GameObject>();

    [SerializeField]
    private RadarLockStrategy radarLockStrategy;

    public float Coverage { get => coverage; set => coverage = value; }
    public List<SAMSiteController> ConnectedSAMSites { get => connectedSAMSites; }

    protected override void Start()
    {
        base.Start();
        radarLockStrategy = new FairLockStrategy();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        coverageAreaCircle.transform.localScale = new Vector3(2 * Coverage, 0.01f, 2 * Coverage);

    }

    public void onMouseClick(string actionName)
    {
        Debug.Log("Mouse clicked on Me with Action: " + actionName);
    }

    public bool connectSamSite(SAMSiteController aSAMSiteController)
    {
        if (!ConnectedSAMSites.Contains(aSAMSiteController) && Vector3.Distance(transform.position, aSAMSiteController.transform.position) < Coverage)
        {
            ConnectedSAMSites.Add(aSAMSiteController);
            return true;
        }
        else
        {
            return false;
        }

    }

    public void removeSamSite(SAMSiteController aSAMSiteController)
    {
        ConnectedSAMSites.Remove(aSAMSiteController);
    }

    internal void addMissileTarget(GameObject missile)
    {
        if (!missileTargets.Contains(missile))
        {
            missileTargets.Add(missile);
        }
    }

    internal void lockTargets()
    {
        List<GameObject>.Enumerator missileTargetEnum = new List<GameObject>(missileTargets.FindAll(e => e != null)).GetEnumerator();
        Queue<SAMSiteController> samSites = new Queue<SAMSiteController>(connectedSAMSites.FindAll(e => e != null));
        radarLockStrategy.lockTargets(missileTargetEnum, samSites);
    }

}
