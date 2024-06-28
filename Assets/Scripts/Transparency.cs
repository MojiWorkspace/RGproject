using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Transparency : MonoBehaviour
{
    public float changeSpeed;
    public float defaultTransparency;
    private float updatedTransparency;
    private Transform[] allChildren;
    void Start()
    {
        allChildren = transform.GetComponentsInChildren<Transform>();

        // Это нужно переделать
        foreach (var item in allChildren)
        {
            if (item.GetComponent<TextMeshPro>() != null)
            {
                Color color =  item.GetComponent<TextMeshPro>().color;
                color.a = defaultTransparency;
                item.GetComponent<TextMeshPro>().color = color;
            }
            else if (item.GetComponent<SpriteRenderer>() != null)
            {
                Color color =  item.GetComponent<SpriteRenderer>().material.color;
                color.a = defaultTransparency;
                item.GetComponent<SpriteRenderer>().material.SetColor("_Color", color);
            }
            else if (item.GetComponent<Image>() != null)
            {
                Color color =  item.GetComponent<Image>().color;
                color.a = defaultTransparency;
                item.GetComponent<Image>().color = color;
            }
        }
    }

    void Update()
    {
        if (updatedTransparency >= 255) updatedTransparency = 255;

        if (updatedTransparency <= 0) updatedTransparency = 0;

        // Это нужно переделать
        if (updatedTransparency >= 0 & updatedTransparency <= 255)
        {
            foreach (var item in allChildren)
            {   updatedTransparency += changeSpeed;
                if (item.GetComponent<TextMeshPro>() != null)
                {
                    Color color =  item.GetComponent<TextMeshPro>().color;
                    color.a = updatedTransparency/255f;
                    item.GetComponent<TextMeshPro>().color = color;
                }
                else if (item.GetComponent<SpriteRenderer>() != null)
                {
                    Color color = item.GetComponent<SpriteRenderer>().material.color;
                    color.a = updatedTransparency/255f;
                    item.GetComponent<SpriteRenderer>().material.SetColor("_Color", color);
                }
                else if (item.GetComponent<Image>() != null)
                {
                    Color color = item.GetComponent<Image>().color;
                    color.a = updatedTransparency/255f;
                    item.GetComponent<Image>().color = color;
                }
            }
        }
    }
}
