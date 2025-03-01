using UnityEngine;
using System.Collections;
using System.IO;
using TMPro;

public class ModifierScript : MonoBehaviour
{
    public ModifierCard modifier;
   //Highlight Variables
    private Material material;    
    private Color MouseOverColor = Color.white;
    public float brightness = 0.5f;
    Color OriginalColor;
    void OnMouseOver()
    {
        //Highlight card
        material.SetColor("_EmissionColor", MouseOverColor * brightness);
        Debug.Log(modifier.Name);
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
        //Setup Modifier Class
        modifier = new ModifierCard("NamePLACEHOLDER","DescriptionPLACEHOLDER",true);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
