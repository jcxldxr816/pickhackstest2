using UnityEngine;
using System.Collections;
using System.IO;
using TMPro;

public class CardController : MonoBehaviour
{
//Tooltip variables
    public TextMeshProUGUI TooltipText;
    public GameObject Tooltip;
    //General attributes
    public string cardName = "CARDNAME";
    public string cardDesc = "CARDDESCRIPTION";
    public string cardType = "modifier";
    public ModifierCard modifier;
    public OffenseCard offense;
    public SupportCard support;

    public int health = 0;
    public int damage = 0;
   //Highlight Variables
    private Material material;    
    private Color MouseOverColor = Color.white;
    private float brightness = 0.2f;
    Color OriginalColor;
    void OnMouseOver()
    {
        //Highlight card
        material.SetColor("_EmissionColor", MouseOverColor * brightness);
        Tooltip.SetActive(true);

    }
    void OnMouseExit()
    {
        //Unhighlight
        material.SetColor("_EmissionColor", OriginalColor);
        Tooltip.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Setup highlight card on mouseover
        material = GetComponent<Renderer>().material;
        Material TooltipBGMaterial;
        OriginalColor = material.GetColor("_EmissionColor");
        //Set up tooltip
        Tooltip.SetActive(false);
        TooltipText.text = cardName + ": \n" + cardDesc;
        if (cardType == "modifier")
        {
           
            Tooltip.GetComponent<Renderer>().material = Resources.Load<Material>("ModifierMaterial");
            MouseOverColor = Color.blue;
             modifier = new ModifierCard(cardName,cardDesc,true);
        }
        if (cardType == "offense")
        {
             Tooltip.GetComponent<Renderer>().material = Resources.Load<Material>("OffenseMaterial");
            MouseOverColor = Color.red;
             offense = new OffenseCard(cardName,cardDesc,damage,health,true);
        }
        if (cardType == "support")
        {
             Tooltip.GetComponent<Renderer>().material = Resources.Load<Material>("SupportMaterial");
            MouseOverColor = Color.green;
            support = new SupportCard(cardName,cardDesc,health,true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Tooltip.transform.position = transform.position + new Vector3(0,-4.0f,0);
        TooltipText.transform.position = transform.position + new Vector3(0,-7f,0);
    }
}
