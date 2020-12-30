using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour, Destroyable
{
    [SerializeField]
    private EMissileType missileType;

    [SerializeField]
    private float turnAngle;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float killRadius;

    [SerializeField]
    private int timeToLive;

    private float lifeTimeCounter;

    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private Vector3 targetObjectPosition;

    [SerializeField]
    private GameObject missileBody;

    [SerializeField]
    private GameObject radarIcon;

    public EMissileType MissileType { get => missileType; set => missileType = value; }

    // Start is called before the first frame update
    void Start()
    {
        lifeTimeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeCounter += Time.deltaTime;
        if (lifeTimeCounter >= timeToLive)
        {
            destroyMe();
        }

        //if targetObject is destroyed, use the latest transform
        if (targetObject != null)
        {
            targetObjectPosition = targetObject.transform.position;
        }

        //if targetposition not initialized, destroy yourself
        if (targetObjectPosition == null)
        {
            destroyMe();
            return;
        }

        if (Vector3.Distance(targetObjectPosition, missileBody.transform.position) <= killRadius)
        {
            if (targetObject != null)
            {
                Destroyable destroyable = targetObject.GetComponent<Destroyable>();
                if (destroyable != null)
                {
                    destroyable.destroyMe();
                }
                else
                {
                    Debug.LogError("TargetObject doesn't have destroyable: " + targetObject.name);
                }
            }
            destroyMe();
        }
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation(targetObjectPosition - missileBody.transform.position);
            missileBody.transform.rotation = Quaternion.RotateTowards(missileBody.transform.rotation, lookRotation, turnAngle * Time.deltaTime);
            missileBody.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            radarIcon.transform.position = missileBody.transform.position;
        }
    }

    public void setParams(GameObject targetObject, EMissileType missileType, float speed, float turnAngle, float killRadius, int timeToLive)
    {
        this.targetObject = targetObject;
        this.missileType = missileType;
        this.targetObjectPosition = this.targetObject.transform.position;
        this.speed = speed;
        this.turnAngle = turnAngle;
        this.killRadius = killRadius;
        this.timeToLive = timeToLive;
    }

    internal GameObject getMissileBody()
    {
        return missileBody;
    }
    public void destroyMe()
    {
        Destroy(gameObject);
    }
}
