using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlItemController : MonoBehaviour
{
    //Our universal inputcontroller
    protected GameObject inputController;

    // Start is called before the first frame update
    public void Start()
    {
        inputController = GameObject.FindGameObjectWithTag("InputController");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

