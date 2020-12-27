using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherController : MonoBehaviour, ActionItemActionHandler, Destroyable
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
    private EMissileType missileType;

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
            Debug.Log("Fire Missile");
            GameObject missile = Instantiate(missilePrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
            missile.GetComponent<MissileController>().setParams(targetObject, missileType, missileSpeed, missileTurnAngle, missileKillRadius);
            GameObject.Find("CommandCenter").GetComponent<CommandCenterController>().addItem(missile);
            fireRate = 0;
        }
    }

    public void onMouseClick(string actionName)
    {
        Debug.Log("Missile launcher clicked");
    }

    public void destroyMe()
    {
        Destroy(gameObject);
    }
}
