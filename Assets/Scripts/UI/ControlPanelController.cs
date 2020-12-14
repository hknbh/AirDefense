using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelController : MonoBehaviour
{
    [SerializeField]
    private bool isMainControlPanel;

    [SerializeField]
    private GameObject backButton;
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

    private void showOnlyComponents()
    {
        for (int index = 0; index < transform.childCount; ++index)
        {
            Transform child = transform.GetChild(index);
            if ((isMainControlPanel && child.gameObject == backButton) || child.GetComponent<ControlPanelController>() != null)
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
            Debug.Log("Child name to hide: " + child.gameObject.name);
            child.gameObject.SetActive(false);
        }
    }




}
