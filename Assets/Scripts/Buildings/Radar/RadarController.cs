using System.Collections;
using UnityEngine;


public class RadarController : MonoBehaviour, ActionItemActionHandler, Destroyable
{

    [SerializeField]
    private bool radarOn;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void setOnOff()
    {
        radarOn = !radarOn;
        Debug.Log("Radar is:" + radarOn);
    }

    public void onMouseClick(string actionName)
    {
        Debug.Log("Mouse clicked on Me with Action: " + actionName);
        setOnOff();
    }

    public void destroyMe()
    {
        Destroy(gameObject);
    }
}
