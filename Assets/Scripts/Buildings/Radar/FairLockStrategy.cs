using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//Creates 1:1 mapping betweeen SAMSites and Missiles. Locks idle SAMSites to the latest missile
public class FairLockStrategy : RadarLockStrategy
{
    public void lockTargets(List<GameObject>.Enumerator misileEnumerator, Queue<SAMSiteController> samSites)
    {
        bool hasNext = misileEnumerator.MoveNext();
        while (hasNext && samSites.Count > 0)
        {
            GameObject missile = misileEnumerator.Current;
            SAMSiteController samSite = samSites.Dequeue();
            if (samSite.lockTarget(missile))
            {
                hasNext = misileEnumerator.MoveNext();
            }

            //if there are no more missiles and are idle samsites, lock them to the latest missile
            if (!hasNext)
            {
                while (samSites.Count > 0)
                {
                    samSite = samSites.Dequeue();
                    samSite.lockTarget(missile);
                }
            }
        }
    }
}
