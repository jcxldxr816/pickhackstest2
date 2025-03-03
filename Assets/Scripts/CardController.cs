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

    public bool isPlayerOwned = true;

    public int health = 0;
    public int brambage = 0;
   //Highlight Variables
    private Material material;    
    private Color MouseOverColor = Color.white;
    private float brightness = 0.1f;
    Color OriginalColor;
    void OnMouseOver()
    {
        if(isPlayerOwned == true)
        {
        //Highlight card
        material.SetColor("_EmissionColor", MouseOverColor * brightness);
        Tooltip.SetActive(true);
        }

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
            // modifier = new ModifierCard(cardName,cardDesc,isPlayerOwned);
        }
        if (cardType == "offense")
        {
             Tooltip.GetComponent<Renderer>().material = Resources.Load<Material>("OffenseMaterial");
            MouseOverColor = Color.red;
             //offense = new OffenseCard(cardName,cardDesc,brambage,health,isPlayerOwned);
        }
        if (cardType == "support")
        {
             Tooltip.GetComponent<Renderer>().material = Resources.Load<Material>("SupportMaterial");
            MouseOverColor = Color.green;
           // support = new SupportCard(cardName,cardDesc,health,isPlayerOwned);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Tooltip.transform.position = transform.position + new Vector3(0,-4.0f,-2.0f);
        TooltipText.transform.position = transform.position + new Vector3(0,-7f,-3.0f);
    }
}
