using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectionScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseSelectedPlane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select()
    {
        mouseSelectedPlane.SetActive(true);
    }

    public void deSelect()
    {
        mouseSelectedPlane.SetActive(false);
    }
}
