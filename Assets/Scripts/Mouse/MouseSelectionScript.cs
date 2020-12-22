using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectionScript : MonoBehaviour, ActionPanelHolder
{
    //Our universal inputcontroller
    [SerializeField]
    private GameObject mainControlPanel;

    //The panel that will be displayed when the object is selected
    [SerializeField]
    private GameObject actionControlPanel;

    [SerializeField]
    private bool isSelectable;

    // if this item has an action
    [SerializeField]
    private bool hasAction;

    //the plane that is displayed when the item is selected
    [SerializeField]
    private GameObject mouseSelectedPlane;

    //Action to be called when this item is selected and users click onto sth
    private Action<GameObject> targetAction;

    public bool IsSelectable { get => isSelectable; set => isSelectable = value; }
    public bool HasAction { get => hasAction; set => hasAction = value; }
    public Action<GameObject> TargetAction { get => targetAction; set => targetAction = value; }

    // Start is called before the first frame update
    public void Start()
    {
        mainControlPanel = GameObject.FindGameObjectWithTag("MainControlPanel");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool select()
    {
        Debug.Log("Is selectable: " + IsSelectable);
        if (IsSelectable)
        {
            mouseSelectedPlane.SetActive(true);
            if (actionControlPanel != null)
            {
                mainControlPanel.GetComponent<ControlPanelController>().openSubControlPanel(actionControlPanel);
                actionControlPanel.GetComponent<ControlPanelController>().setActionItemActionHandler(gameObject);
            }
        }
        return IsSelectable;
    }

    public void deSelect()
    {
        mouseSelectedPlane.SetActive(false);
        Debug.Log("MainControlPanel: " + mainControlPanel.GetComponent<ControlPanelController>());
        mainControlPanel.GetComponent<ControlPanelController>().showOnlyComponents();
    }

    public void setActionPanel(GameObject aActionControlPanel)
    {
        actionControlPanel = aActionControlPanel;
    }
}
