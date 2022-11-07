using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandHealth : Health
{
    public GameObject treasure;
    public GameObject powerUp;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        OnDeath();
    }
    public override void OnDeath ()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Called");
            Instantiate(powerUp, treasure.GetComponent<Transform>().position, treasure.GetComponent<Transform>().rotation);

            treasure.transform.parent = null;
            gameObject.SetActive(false);
            treasure.SetActive(false);


        }
    }


}
