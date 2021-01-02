using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface RadarLockStrategy
{
    void lockTargets(List<GameObject>.Enumerator misileEnumerator, Queue<SAMSiteController> samSites);
}
