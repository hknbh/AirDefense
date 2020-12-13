using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectionScript : MonoBehaviour
{
    [SerializeField]
    private bool isSelectable;

    // if this item has an action
    [SerializeField]
    private bool hasAction;

    [SerializeField]
    private GameObject mouseSelectedPlane;

    //Action to be called when this item is selected and users click onto sth
    private Action<GameObject> targetAction;

    public bool IsSelectable { get => isSelectable; set => isSelectable = value; }
    public bool HasAction { get => hasAction; set => hasAction = value; }
    public Action<GameObject> TargetAction { get => targetAction; set => targetAction = value; }

    // Start is called before the first frame update
    void Start()
    {
        IsSelectable = true;
        HasAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool select()
    {
        Debug.Log("Is selectable: " + IsSelectable);
        if (IsSelectable)
        {
            mouseSelectedPlane.SetActive(true);
        }
        return IsSelectable;
    }

    public virtual void deSelect()
    {
        mouseSelectedPlane.SetActive(false);
    }
}
