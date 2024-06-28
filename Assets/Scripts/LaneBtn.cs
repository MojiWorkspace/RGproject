using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBtn : MonoBehaviour
{
    public static LaneBtn Instance;
    public KeyCode input;
    public Color outlineColor;
    public bool isActive;
    private Color colorDefault = new Color(100/255f, 100/255f, 100/255f, 1);
    private Color colorOnPress = new Color(60/255f, 60/255f, 60/255f, 1);

    void Start()
    {
        GetComponent<SpriteRenderer>().color = colorDefault;
        GetComponent<SpriteRenderer>().material.SetColor("_SolidOutline", outlineColor);
    }

    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(input))
            {
                GetComponent<SpriteRenderer>().color = colorOnPress;
                GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 1);
            }

            if (Input.GetKeyUp(input))
            {
                GetComponent<SpriteRenderer>().color = colorDefault;
                GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 0);
            }
        }

    }
}
