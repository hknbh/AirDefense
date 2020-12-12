using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Mouse
{
    public class MouseController : MonoBehaviour
    {
        private GameObject selectedObject;

        private InteractionType interactionType;

        private GameObject itemToAdd;

        // Start is called before the first frame update
        void Start()
        {
            interactionType = InteractionType.SELECTION;
        }

        // Update is called once per frame
        void Update()
        {
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
                switch (interactionType)
                {
                    case InteractionType.SELECTION: selectItem(); break;
                    case InteractionType.ADD_ITEM: addItem(); break;
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

        public void addItemMode(GameObject building)
        {
            interactionType = InteractionType.ADD_ITEM;
            itemToAdd = building;
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
                    }
                }
            }
            else
            {
                deSelectCurrent();
            }

        }

        private void deSelectCurrent()
        {
            if (selectedObject != null)
            {
                selectedObject.GetComponent<MouseSelectionScript>().deSelect();
                selectedObject = null;
            }
        }

        public RaycastHit getRayCastHit(out bool aIsHit)
        {
            RaycastHit raycastHit = new RaycastHit();
            aIsHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit);
            return raycastHit;
        }

    }
}
