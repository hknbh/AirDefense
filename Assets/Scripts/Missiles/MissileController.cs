using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
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
        if (targetObject == null)
        {
            return;
        }
        if (Vector3.Distance(targetObject.transform.position, missileBody.transform.position) <= killRadius)
        {
            Destroy(targetObject);
            Destroy(this.gameObject);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetObject.transform.position - missileBody.transform.position);
            missileBody.transform.rotation = Quaternion.RotateTowards(missileBody.transform.rotation, targetRotation, turnAngle * Time.deltaTime);
            missileBody.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            radarIcon.transform.position = missileBody.transform.position;
        }
    }

    public void setParams(GameObject targetObject, float speed, float turnAngle, float killRadius)
    {
        this.targetObject = targetObject;
        this.speed = speed;
        this.turnAngle = turnAngle;
        this.killRadius = killRadius;
    }
}
