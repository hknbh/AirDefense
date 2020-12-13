﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour
{
    [SerializeField]
    private GameObject missilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireMissile(GameObject targetObject)
    {
        GameObject missile = Instantiate(missilePrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        missile.GetComponent<MissileScript>().setParams(targetObject, 50, 5, 10);
        Debug.Log("Missile Target: " + targetObject.transform.position);
    }

   
}