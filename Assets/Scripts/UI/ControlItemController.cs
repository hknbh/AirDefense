using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlItemController : MonoBehaviour
{
    //Our universal inputcontroller
    protected GameObject inputController;

    protected ResourceManager resourceManager;

    // Start is called before the first frame update
    public void Start()
    {
        inputController = GameObject.FindGameObjectWithTag("InputController");
        resourceManager = GameObject.Find("CommandCenter").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

