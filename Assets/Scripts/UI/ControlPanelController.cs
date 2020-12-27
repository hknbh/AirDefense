using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelController : MonoBehaviour
{
    [SerializeField]
    private bool isMainControlPanel;

    // Start is called before the first frame update
    void Start()
    {
        showOnlyComponents();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openSubControlPanel(GameObject subControlPanel)
    {
        hideAllComponents();
        transform.Find(subControlPanel.name).gameObject.SetActive(true);
    }

    public void back()
    {
        gameObject.SetActive(false);
        GetComponentInParent<ControlPanelController>().showOnlyComponents();
    }

    public void showOnlyComponents()
    {
        for (int index = 0; index < transform.childCount; ++index)
        {
            Transform child = transform.GetChild(index);
            if (child.GetComponent<ControlPanelController>() != null)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private void hideAllComponents()
    {
        for (int index = 0; index < transform.childCount; ++index)
        {
            Transform child = transform.GetChild(index);
            child.gameObject.SetActive(false);
        }
    }

    internal void setActionItemActionHandler(GameObject gameObject)
    {
        for (int index = 0; index < transform.childCount; ++index)
        {
            Transform child = transform.GetChild(index);
            ActionItemController actionItemController = child.GetComponent<ActionItemController>();
            if (actionItemController != null)
            {
                actionItemController.setActionObject(gameObject);
            }
        }
    }
}
