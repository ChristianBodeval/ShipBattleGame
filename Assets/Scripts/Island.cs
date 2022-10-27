using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public float health;
    public GameObject treasure;
    public GameObject powerUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnDeath();
    }
    public void OnDeath ()
    {
        if (health <= 0)
        {

            Instantiate(powerUp, treasure.GetComponent<Transform>().position, treasure.GetComponent<Transform>().rotation);
            treasure.transform.parent = null;
            Debug.Log("bababoi");
            gameObject.SetActive(false);
            treasure.SetActive(false);


        }
    }
}
