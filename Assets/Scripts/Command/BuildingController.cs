using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Mouse;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    private GameObject inputController;

    [SerializeField]
    private GameObject objectSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void addRadar()
    {
        Debug.Log("Add radar selected");
        MouseController mouseController = inputController.GetComponent<MouseController>();
        RaycastHit raycastHit = mouseController.getRayCastHit(out bool isHit);
        GameObject radar = objectSpawner.GetComponent<BuildingSpawner>().spawnRadar(raycastHit.point);
        mouseController.addItemMode(radar);
    }
}
