using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water_background : MonoBehaviour
{

    //public Image background;
    public int startPosY = -1960;

    // Start is called before the first frame update
    void Start()
    {
        //background = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, startPosY++);
    }
}
