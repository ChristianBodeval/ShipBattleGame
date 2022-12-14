using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShipHealthBar : MonoBehaviour
{
    //This scripts makes sure the healthbar doesen't turn with the merchantship

    public GameObject healthBar;
    public Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = transform.root.position + offset;
        healthBar.transform.rotation = Quaternion.identity;        
    }
}
