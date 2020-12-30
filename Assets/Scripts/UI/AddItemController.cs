using System.Collections;
using UnityEngine;


public class AddItemController : ControlItemController
{
    [SerializeField]
    private GameObject objectPrefab;

    [SerializeField]
    private GameObject actionPanel;

    [SerializeField]
    private GameObject subControlPanel;

    public void addItem()
    {
        CostOwner costOwner = objectPrefab.GetComponent<CostOwner>();
        if(costOwner == null)
        {
            Debug.LogError("No cost owner found");
        }

        if (resourceManager.getResources() >= costOwner.getCost())
        {
            MouseController mouseController = inputController.GetComponent<MouseController>();
            RaycastHit raycastHit = mouseController.getRayCastHit(out bool isHit);
            GameObject itemToAdd = Instantiate(objectPrefab, raycastHit.point, Quaternion.identity);
            if (actionPanel != null)
            {
                itemToAdd.GetComponent<ActionPanelHolder>().setActionPanel(actionPanel);
            }
            mouseController.addItemMode(itemToAdd);

            resourceManager.spendResource(costOwner.getCost());
        }
       
    }

    public void openSubControlPanel()
    {
        ControlPanelController parentController = GetComponentInParent<ControlPanelController>();
        parentController.openSubControlPanel(subControlPanel);
    }
}
