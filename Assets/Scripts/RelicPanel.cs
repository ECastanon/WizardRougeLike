using UnityEngine;
using TMPro;

public class RelicPanel : MonoBehaviour
{
    private Animator anim;

    [Header("RelicCardData")]
    //Used to change border color based on rarity
    public GameObject relicCard1;
    public GameObject relicCard2;
    public GameObject relicCard3;
    //Used to change RC info based on its item
    //Name
    public TextMeshProUGUI rctitle1;
    public TextMeshProUGUI rctitle2;
    public TextMeshProUGUI rctitle3;
    //Sprite
    public GameObject rcsprite1;
    public GameObject rcsprite2;
    public GameObject rcsprite3;
    //Description
    public TextMeshProUGUI rcdesc1;
    public TextMeshProUGUI rcdesc2;
    public TextMeshProUGUI rcdesc3;

    private bool inView = false;

    //RC values
    public GameObject spriteContainer;
    private SpriteRenderer newSprite;
    private int RCNum = 1;
    private int rcRand;
    private string rarity = "";
    private string descValue1 = "";
    private string descValue2 = "";

    public string roomType;

    [Header("Super")]
    public GameObject vampireTooth; //30% chance to gain 1 hp from defeating an enemy
    public GameObject medalofValor; //Heals 1hp after completing a room
    public GameObject scrollofMight; //Increases all weapon damages by +1

    [Header("Ultra")]
    public GameObject holyEmbrace; //Protects from the first attack in every new room
    public GameObject AncientSigil; //Reduces all weapon CDs by 30%
    public GameObject chargeStone; //Gain an additional charge on your strong attack

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    //Called In EnemyCounter
    public void SlideInMenu()
    {
        GenerateRelic(relicCard1, rctitle1, rcsprite1, rcdesc1);
        GenerateRelic(relicCard2, rctitle2, rcsprite2, rcdesc2);
        GenerateRelic(relicCard3, rctitle3, rcsprite3, rcdesc3);

        if (!inView)
        {
            anim.Play("RelicPanelSlideIn");
            inView = true;
            Time.timeScale = 0f;
        }
    }

    public void SlideOutMenu()
    {
        if (inView)
        {
            anim.Play("RelicPanelSlideOut");
            inView = false;
            Time.timeScale = 1f;
        }
    }

    public void GenerateRelic(GameObject rc, TextMeshProUGUI rcTitle, GameObject rcSprite, TextMeshProUGUI rcDesc)
    {
        float rand = Random.Range(0.0f, 1.0f);
        RCNum += 1;

        if (roomType == "Basic" || roomType == "Untagged")
        {
            if (rand <= .05f) //5%
            {
                //Super Relic
                rarity = "Super";
            }
            else if (.05f < rand && rand <= .20f) //15%
            {
                //Rare Relic
                rarity = "Rare";

            }
            else if (.20f < rand && rand <= .50f) //30%
            {
                //Uncommon Relic
                rarity = "Uncommon";

            }
            else if (.50f < rand) //50%
            {
                //Common Relic
                rarity = "Common";
            }
        }
        Debug.Log("RC" + (RCNum-1) + " = " + rand + " AND my rarity is: " + rarity);

        //Creation of the Relic
        ChangeRCBorderColor(rc);
        GetRC(rc, rcTitle, rcSprite, rcDesc);
    }

    private void ChangeRCBorderColor(GameObject rc)
    {
        GameObject border;
        switch (rarity)
        {
            //Change Border Color to Gray
            case "Common":
                border = rc.transform.GetChild(1).gameObject;
                //border.GetComponent<SpriteRenderer>().color = new Color(183, 183, 183);
                border.GetComponent<SpriteRenderer>().color = new Color(.717f, .717f, .717f);
                break;
            //Change Border Color to Green
            case "Uncommon":
                border = rc.transform.GetChild(1).gameObject;
                //border.GetComponent<SpriteRenderer>().color = new Color(78, 241, 60);
                border.GetComponent<SpriteRenderer>().color = new Color(.305f, .945f, .235f);
                break;
            //Change Border Color to Blue
            case "Rare":
                border = rc.transform.GetChild(1).gameObject;
                //border.GetComponent<SpriteRenderer>().color = new Color(15, 131, 255);
                border.GetComponent<SpriteRenderer>().color = new Color(.058f, .521f, 1);
                break;
            //Change Border Color to Red
            case "Super":
                border = rc.transform.GetChild(1).gameObject;
                //border.GetComponent<SpriteRenderer>().color = new Color(233, 25, 0);
                border.GetComponent<SpriteRenderer>().color = new Color(.913f, .098f, 0);

                break;
            //Change Border Color to Purple
            case "Ultra":
                border = rc.transform.GetChild(1).gameObject;
                //border.GetComponent<SpriteRenderer>().color = new Color(160, 0, 231);
                border.GetComponent<SpriteRenderer>().color = new Color(.627f, 0, .905f);

                break;
            //Change Border Color to Yellow
            case "Sacred":
                border = rc.transform.GetChild(1).gameObject;
                //border.GetComponent<SpriteRenderer>().color = new Color(255, 213, 51);
                border.GetComponent<SpriteRenderer>().color = new Color(1, .835f, .2f);
                break;
            default:
                Debug.Log("Improper rarity!");
                break;
        }
    }
    private void GetRC(GameObject rc, TextMeshProUGUI rcTitle, GameObject rcSprite, TextMeshProUGUI rcDesc)
    {
        //Returns 0-99
        //int rand = Random.Range(0, 100);

        //"<color=#50C878>This is green!</color>"

        //Positive values use Emerald Green - #50C878
        //Negative values used Scarlet - #FF2400
        Debug.Log(rarity);
        switch (rarity)
        {
            case "Common":
                rcRand = Random.Range(0, 3);
                //EtherealStone
                if(rcRand == 0)
                {
                    descValue1 = "<color=#50C878>1.5x</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Ethereal Stone";
                    newSprite = spriteContainer.transform.Find("EtherealStone").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Increases dash distance by " + descValue1;
                }
                //EtherealShockPendant
                if (rcRand == 1)
                {
                    descValue1 = "<color=#50C878>1</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Ethereal Shock Pendant";
                    newSprite = spriteContainer.transform.Find("EtherealShockPendant").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Damages enemies dodged into by " + descValue1;
                }
                //SoulJar
                if (rcRand == 2)
                {
                    descValue1 = "<color=#50C878>1</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Soul Jar";
                    newSprite = spriteContainer.transform.Find("SoulJar").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Increases maxHP by " + descValue1 + " for every 5 enemies defeated";
                }
                break;
            case "Uncommon":
                //Augur'sTalisman - Shortens negative status effects by 15%
                //CosmoRing - Reduces Strong attacks CD by 5% per item complete
                //Monocle - Additional 5% EXP gain from defeating enemies
                rcRand = Random.Range(0, 3);
                //Augur'sTalisman
                if (rcRand == 0)
                {
                    descValue1 = "<color=#50C878>15%</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Augur's Talisman";
                    newSprite = spriteContainer.transform.Find("Augur'sTalisman").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Shortens negative status effects by " + descValue1;
                }
                //CosmoRing
                if (rcRand == 1)
                {
                    descValue1 = "<color=#50C878>5%</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Cosmo Ring";
                    newSprite = spriteContainer.transform.Find("CosmoRing").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Reduces strong attack Cooldowns by " + descValue1;
                }
                //Monocle
                if (rcRand == 2)
                {
                    descValue1 = "<color=#50C878>5%</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Monocle";
                    newSprite = spriteContainer.transform.Find("Monocle").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Increase EXP gain by " + descValue1;
                }
                break;
            case "Rare":
                //
                break;
            case "Super":
                rcRand = Random.Range(0, 3);
                //VampireTooth
                if (rcRand == 0)
                {
                    descValue1 = "<color=#50C878>1</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Vampire Tooth";
                    newSprite = spriteContainer.transform.Find("VampireTooth").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "30% chance to recover HP by " + descValue1 + " after defeating an enemy";
                }
                //MedalofValor
                if (rcRand == 1)
                {
                    descValue1 = "<color=#50C878>2</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Medal of Valor";
                    newSprite = spriteContainer.transform.Find("MedalofValor").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Recover " + descValue1 + " HP after clearing a room";
                }
                //ScrollofMight
                if (rcRand == 2)
                {
                    descValue1 = "<color=#50C878>+1</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Scroll of Might";
                    newSprite = spriteContainer.transform.Find("ScrollofMight").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Increase all weapon damage by " + descValue1;
                }
                break;
            case "Ultra":
                //
                break;
            case "Sacred":
                //
                break;
            default:
                Debug.Log("Improper Card!");
                break;
        }
    }
}
