using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBodyController : MonoBehaviour, Damagable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Terrain"))
        {
            damage(1);
        }
    }

    public void damage(int damageLevel)
    {
        Destroy(transform.parent.gameObject);
    }
}
