using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour, Damagable
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
            damage(1);
        }

        //if targetObject is destroyed, use the latest transform
        if (targetObject != null)
        {
            targetObjectPosition = targetObject.transform.position;
        }

        //if targetposition not initialized, destroy yourself
        if (targetObjectPosition == null)
        {
            damage(1);
            return;
        }

        if (Vector3.Distance(targetObjectPosition, missileBody.transform.position) <= killRadius)
        {
            if (targetObject != null)
            {
                Damagable damagable = targetObject.GetComponent<Damagable>();
                if (damagable != null)
                {
                    damagable.damage(1);
                }
                else
                {
                    Debug.LogError("TargetObject doesn't have destroyable: " + targetObject.name);
                }
            }
            damage(1);
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
    public void damage(int damageLevel)
    {
        GameObject.Find("CommandCenter").GetComponent<CommandCenterController>().removeItem(gameObject);
        Destroy(gameObject);
    }
}
