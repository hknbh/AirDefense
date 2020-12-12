using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject radarPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject spawnRadar(Vector3 aPosition)
    {
        return Instantiate(radarPrefab, aPosition, Quaternion.identity);
    }
}
