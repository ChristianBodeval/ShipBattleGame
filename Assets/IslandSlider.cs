using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IslandSlider : MonoBehaviour
{

    GameObject island;
    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        island = transform.root.gameObject;
        slider.maxValue = island.GetComponent<Health>().StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = island.GetComponent<Health>().CurrentHealth;
    }
}

