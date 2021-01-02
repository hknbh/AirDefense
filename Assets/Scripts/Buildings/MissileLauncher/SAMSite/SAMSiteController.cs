using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAMSiteController : MissileLauncherController
{

    [SerializeField]
    private bool locked = false;

    [SerializeField]
    private GameObject targetMissile;

    private LineRenderer lineRenderer;

    public bool Locked { get => locked; }

    protected override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    protected override void updateAttack()
    {
        if (Locked && targetMissile != null)
        {
            drawLineToTarget(targetMissile);
            fireMissile(targetMissile);
        }
        else
        {
            targetMissile = null;
            locked = false;
            deleteTargetLine();
        }
    }

    public override bool lockTarget(GameObject missile)
    {
        MissileController missileController = missile.GetComponent<MissileController>();
        if (missileController != null)
        {
            if (VectorUtils.Vector2Distance(transform.position, missileController.getMissileBody().transform.position) <= Range)
            {
                targetMissile = missileController.getMissileBody();
                locked = true;
                return true;
            }
            return false;
        }
        else
        {
            Debug.LogError("SAM Target is not a missile");
            return false;
        }
    }


    private void drawLineToTarget(GameObject targetObject)
    {
        if (targetObject != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] { transform.position, targetObject.transform.position });
        }
    }

    private void deleteTargetLine()
    {
        lineRenderer.positionCount = 0;
    }

    //SAM should look to the target on firing to have more accuracy 
    //therefore I set the rotation to the target instead of air (as in base method)
    protected override GameObject instantiateMissile(GameObject targetObject)
    {
        GameObject missile = base.instantiateMissile(targetObject);
        Quaternion lookRotation = Quaternion.LookRotation(targetObject.transform.position - missile.transform.position);
        missile.GetComponent<MissileController>().getMissileBody().transform.rotation = lookRotation;
        return missile;
    }
}
