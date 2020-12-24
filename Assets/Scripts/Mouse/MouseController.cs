using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseController : MonoBehaviour
{
    private GameObject selectedObject;

    private InteractionType interactionType;

    //Item to be added into the map
    private GameObject itemToAdd;

    //When item is selected and clicked on the map this action will be run
    private Action<GameObject> targetActionLambda;

    // Start is called before the first frame update
    void Start()
    {
        interactionType = InteractionType.SELECTION;
    }

    // Update is called once per frame
    void Update()
    {
        //ignore if mouse is over an UI component
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (interactionType == InteractionType.ADD_ITEM)
        {
            addItemUpdate();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Interaction Type: " + interactionType);
            switch (interactionType)
            {
                case InteractionType.SELECTION: selectItem(); break;
                case InteractionType.ADD_ITEM: addItem(); break;
                case InteractionType.TARGET_ACTION: runTargetAction(); break;
            }
        }
    }

    private void runTargetAction()
    {
        if (targetActionLambda != null)
        {
            RaycastHit rayCastHit = getRayCastHit(out bool isHit);
            if (isHit)
            {
                GameObject hitObject = rayCastHit.transform.gameObject;
                targetActionLambda(hitObject);
                targetActionLambda = null;
                interactionType = InteractionType.SELECTION;
                deSelectCurrent();
            }
        }
    }

    private void addItemUpdate()
    {
        if (itemToAdd != null)
        {
            itemToAdd.SetActive(false);
            RaycastHit rayCastHit = getRayCastHit(out bool isHit);
            if (isHit)
            {
                itemToAdd.transform.position = rayCastHit.point;
                itemToAdd.SetActive(true);
                Debug.Log(rayCastHit.point);
            }
        }
        else
        {
            Debug.LogError("No Item to add. Return to selection");
            interactionType = InteractionType.SELECTION;
        }
    }

    public void addItemMode(GameObject aBuilding)
    {
        interactionType = InteractionType.ADD_ITEM;
        itemToAdd = aBuilding;
    }

    private void addItem()
    {
        Debug.Log("Add Item");
        itemToAdd = null;
        interactionType = InteractionType.SELECTION;
    }

    private void selectItem()
    {
        RaycastHit hitInfo = getRayCastHit(out bool isHit);
        if (isHit)
        {
            Debug.Log("Hit to:" + hitInfo.transform.position);
            Debug.Log("Hit point: " + hitInfo.point);
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject != selectedObject)
            {
                deSelectCurrent();
                MouseSelectionScript mouseSelectionScript = hitObject.GetComponent<MouseSelectionScript>();
                if (mouseSelectionScript != null)
                {
                    mouseSelectionScript.select();
                    selectedObject = hitObject;
                    if (mouseSelectionScript.HasAction)
                    {
                        targetActionLambda = mouseSelectionScript.TargetAction;
                        interactionType = InteractionType.TARGET_ACTION;
                    }
                    drawSelectedCircle(hitObject.transform);
                }
            }
        }
        else
        {
            deSelectCurrent();
        }

    }

    private void drawSelectedCircle(Transform transform)
    {
        Material terrainMaterial = Terrain.activeTerrain.materialTemplate;
        terrainMaterial.SetFloat("_isShowSelection", 1);
        terrainMaterial.SetVector("_Center", new Vector4(transform.position.x, transform.position.y, transform.position.z, 0));
    }

    private void deSelectCurrent()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<MouseSelectionScript>().deSelect();
            selectedObject = null;
        }
        Material terrainMaterial = Terrain.activeTerrain.materialTemplate;
        terrainMaterial.SetFloat("_isShowSelection", 0);
    }

    public RaycastHit getRayCastHit(out bool aIsHit)
    {
        RaycastHit raycastHit = new RaycastHit();
        aIsHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit);
        return raycastHit;
    }

}

