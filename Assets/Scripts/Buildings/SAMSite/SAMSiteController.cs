using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAMSiteController : MonoBehaviour
{
    [SerializeField]
    private GameObject missilePrefab;

    [SerializeField]
    private int missileSpeed;

    [SerializeField]
    private int missileTurnAngle;

    [SerializeField]
    private int missileKillRadius;

    [SerializeField]
    private float fireRate = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate < 5)
        {
            fireRate += Time.deltaTime;
        }
    }

    public void fireMissile(GameObject targetObject)
    {
        if (fireRate >= 5)
        {
            fireRate = 0;
            GameObject missile = Instantiate(missilePrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
            missile.GetComponent<MissileController>().setParams(targetObject, missileSpeed, missileTurnAngle, missileKillRadius);
        }
    }
}
