using System.Collections;
using UnityEditor;
using UnityEngine;

public class BuildingController : MonoBehaviour, CostOwner, Damagable, AddableFromControlPanel
{
    [SerializeField]
    private int life;

    [SerializeField]
    private int cost;

    [SerializeField]
    private bool isGainResource;

    [SerializeField]
    private int resourceGainPer10Sec;

    [SerializeField]
    protected GameObject commandCenter;

    protected virtual void Start()
    {
        commandCenter = GameObject.Find("CommandCenter");
    }

    protected virtual void Update()
    {

    }

    private IEnumerator resourceGain()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            commandCenter.GetComponent<ResourceManager>().addResource(resourceGainPer10Sec);
        }
    }

    public int getCost()
    {
        return cost;
    }

    public void damage(int damageLevel)
    {
        life -= damageLevel;
        if (life <= 0)
        {
            commandCenter.GetComponent<CommandCenterController>().removeItem(gameObject);
            Destroy(gameObject);
        }
    }

    public void initItem()
    {
        if (isGainResource)
        {
            StartCoroutine("resourceGain");
        }
    }
}
