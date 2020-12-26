using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour, Destroyable
{
    [SerializeField]
    private float turnAngle = 20;

    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private float killRadius = 10.0f;

    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private Vector3 targetObjectPosition;

    [SerializeField]
    private GameObject missileBody;

    [SerializeField]
    private GameObject radarIcon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
                targetObject.GetComponent<Destroyable>().destroyMe();
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

    public void setParams(GameObject targetObject, float speed, float turnAngle, float killRadius)
    {
        if (targetObject.GetComponent<MissileController>() != null)
        {
            this.targetObject = targetObject.transform.GetChild(0).gameObject;
        }
        else
        {
            this.targetObject = targetObject;
        }
        this.targetObjectPosition = this.targetObject.transform.position;
        this.speed = speed;
        this.turnAngle = turnAngle;
        this.killRadius = killRadius;
    }

    public void destroyMe()
    {
        Destroy(gameObject);
    }
}
