using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherController : BuildingController, ActionItemActionHandler
{

    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private float range;

    [SerializeField]
    private int missileCount;

    private int missileCountCounter;

    [SerializeField]
    private float RELOAD_TIME;

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
    protected override void Start()
    {
        base.Start();
        fireRateCounter = FIRE_RATE;
        missileCountCounter = missileCount;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (fireRateCounter < FIRE_RATE)
        {
            fireRateCounter += Time.deltaTime;
        }
        updateAttack();
    }


    protected virtual void updateAttack()
    {
        if (targetObject != null)
        {
            fireMissile(targetObject);
        }
    }

    public virtual bool lockTarget(GameObject aTargetObject)
    {
        targetObject = aTargetObject;
        return true;
    }

    public virtual bool fireMissile(GameObject targetObject)
    {
        if (canFire())
        {
            GameObject missile = instantiateMissile(targetObject);
            missile.GetComponent<MissileController>().setParams(targetObject, missileType, missileSpeed, missileTurnAngle, missileKillRadius, missileTimeToLive);
            GameObject.Find("CommandCenter").GetComponent<CommandCenterController>().addItem(missile);
            fireRateCounter = 0;
            missileCountCounter--;
            if (missileCountCounter == 0)
            {
                StartCoroutine("reload");
            }
            return true;
        }
        return false;
    }

    protected virtual GameObject instantiateMissile(GameObject targetObject)
    {
        return Instantiate(missilePrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
    }

    public bool canFire()
    {
        return fireRateCounter >= FIRE_RATE && missileCountCounter > 0;
    }

    public void onMouseClick(string actionName)
    {
        Debug.Log("Missile launcher clicked");
    }

    private IEnumerator reload()
    {
        yield return new WaitForSeconds(RELOAD_TIME);
        missileCountCounter = missileCount;
    }

   

}
