using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlItemController : MonoBehaviour
{
    //Our universal inputcontroller
    private GameObject inputController;

    [SerializeField]
    private GameObject objectPrefab;

    [SerializeField]
    private GameObject subControlPanel;

    // Start is called before the first frame update
    void Start()
    {
        inputController = GameObject.FindGameObjectWithTag("InputController");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addItem()
    {
        Debug.Log("Add item selected");
        MouseController mouseController = inputController.GetComponent<MouseController>();
        RaycastHit raycastHit = mouseController.getRayCastHit(out bool isHit);
        GameObject itemToAdd = Instantiate(objectPrefab, raycastHit.point, Quaternion.identity);
        mouseController.addItemMode(itemToAdd);
    }

    public void openSubControlPanel()
    {
        ControlPanelController parentController = GetComponentInParent<ControlPanelController>();
        parentController.openSubControlPanel(subControlPanel);
    }
}

