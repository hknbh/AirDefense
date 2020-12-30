using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherController : MonoBehaviour, ActionItemActionHandler, Destroyable
{

    [SerializeField]
    private float range;
    
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
    private int missileTimeToLive;

    [SerializeField]
    private float FIRE_RATE = 5;

    private float fireRateCounter = 5;

    public float Range { get => range; set => range = value; }

    // Start is called before the first frame update
    protected void Start()
    {
        fireRateCounter = FIRE_RATE;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (fireRateCounter < FIRE_RATE)
        {
            fireRateCounter += Time.deltaTime;
        }
    }

    public virtual bool fireMissile(GameObject targetObject)
    {
        if (canFire())
        {
            GameObject missile = Instantiate(missilePrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
            missile.GetComponent<MissileController>().setParams(targetObject, missileType, missileSpeed, missileTurnAngle, missileKillRadius, missileTimeToLive);
            GameObject.Find("CommandCenter").GetComponent<CommandCenterController>().addItem(missile);
            fireRateCounter = 0;
            return true;
        }
        return false;
    }

    public bool canFire()
    {
        return fireRateCounter >= FIRE_RATE;
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
