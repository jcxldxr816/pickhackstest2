using UnityEngine;
using System.Collections;

public class CardControl : MonoBehaviour
{

    private Material material;    
    public Color MouseOverColor = Color.white;
    public float brightness = 2.0f;
    Color OriginalColor;
    MeshRenderer renderer;
    void OnMouseOver()
    {
        material.SetColor("_EmissionColor", MouseOverColor * brightness);
    }
    void OnMouseExit()
    {
        material.SetColor("_EmissionColor", OriginalColor);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
        OriginalColor = material.GetColor("_EmissionColor");
        MouseOverColor.a = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
