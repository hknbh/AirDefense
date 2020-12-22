using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionItemController : ControlItemController
{

    //name of this action
    [SerializeField]
    private string actionName;

    [SerializeField]
    private GameObject actionObject;

    private ActionItemActionHandler actionItemActionHandler;

    public void setActionObject(GameObject aActionObject)
    {
        actionObject = aActionObject;
        actionItemActionHandler = actionObject.GetComponent<ActionItemActionHandler>();
        if(actionItemActionHandler == null)
        {
            Debug.LogError("Null action item action handler");
        }
    }

    public void onMouseClick()
    {
        actionItemActionHandler.onMouseClick(actionName);
    }


}


    





