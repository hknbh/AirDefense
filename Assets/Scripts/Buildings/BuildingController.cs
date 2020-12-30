using System.Collections;
using UnityEditor;
using UnityEngine;

public class BuildingController : MonoBehaviour, CostOwner, Destroyable
{
    [SerializeField]
    private int cost;

    [SerializeField]
    private bool isGainResource;

    [SerializeField]
    private int resourceGainPer10Sec;

    [SerializeField]
    private GameObject commandCenter;

    protected virtual void Start()
    {
        commandCenter = GameObject.Find("CommandCenter");

        if (isGainResource)
        {
            StartCoroutine(resourceGain());
        }
    }

    protected virtual void Update()
    {

    }

    private IEnumerator resourceGain()
    {
        while (true)
        {
            Debug.Log("Add resource " + resourceGainPer10Sec);
            commandCenter.GetComponent<ResourceManager>().addResource(resourceGainPer10Sec);
            yield return new WaitForSeconds(5);
        }
    }


    public int getCost()
    {
        return cost;
    }

    public void destroyMe()
    {
        Destroy(gameObject);
    }
}
