using UnityEngine;
using System.Collections;
//using System;
using System.IO;

public class CardControl : MonoBehaviour
{
    //Highlight Variables
    private Material material;    
    private Color MouseOverColor = Color.white;
    public float brightness = 0.5f;
    public string cardName;
    public string cardDesc;
    Color OriginalColor;
    void OnMouseOver()
    {
        //Highlight card
        material.SetColor("_EmissionColor", MouseOverColor * brightness);
    }
    void OnMouseExit()
    {
        //Unhighlight
        material.SetColor("_EmissionColor", OriginalColor);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Setup highlight card on mouseover
        material = GetComponent<Renderer>().material;
        OriginalColor = material.GetColor("_EmissionColor");

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
