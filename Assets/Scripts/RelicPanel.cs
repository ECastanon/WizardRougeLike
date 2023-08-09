using UnityEngine;
using TMPro;

public class RelicPanel : MonoBehaviour
{
    private Animator anim;

    public GameObject gameMaster;

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
    private string RCItem1 = "";
    private string RCItem2 = "";
    private string RCItem3 = "";

    [HideInInspector]
    public string roomType;

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
    private void SlideOutMenu()
    {
        if (inView)
        {
            anim.Play("RelicPanelSlideOut");
            inView = false;
            Time.timeScale = 1f;
        }
    }

    public void SelectRC1()
    {
        SlideOutMenu();
        gameMaster.GetComponent<RelicEffects>().ApplyRC(RCItem1);
    }
    public void SelectRC2()
    {
        SlideOutMenu();
        gameMaster.GetComponent<RelicEffects>().ApplyRC(RCItem2);
    }
    public void SelectRC3()
    {
        SlideOutMenu();
        gameMaster.GetComponent<RelicEffects>().ApplyRC(RCItem3);
    }

    //Button for rerolling the given relics
    public void ReRoll()
    {

    }
    //Button for deleting a relic for the rest of the run
    public void Eliminate()
    {

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
                    descValue1 = "<color=#50C878>10%</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Ethereal Stone";
                    newSprite = spriteContainer.transform.Find("EtherealStone").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Increases dash distance by " + descValue1;
                }
                //ShockPendant
                if (rcRand == 1)
                {
                    descValue1 = "<color=#50C878>5</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Shock Pendant";
                    newSprite = spriteContainer.transform.Find("EtherealShockPendant").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Creates a bolt of lightning that deals " + descValue1 + " damage to a random enemy";
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
                rcRand = Random.Range(0, 3);
                //GrowthSerum
                if (rcRand == 0)
                {
                    descValue1 = "<color=#50C878>10%</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Growth Serum";
                    newSprite = spriteContainer.transform.Find("GrowthSerum").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Increases spell size by " + descValue1;
                }
                //HermesSandals
                if (rcRand == 1)
                {
                    descValue1 = "<color=#50C878>15%</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Hermes Sandals";
                    newSprite = spriteContainer.transform.Find("HermesSandals").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Increase movement speed by " + descValue1;
                }
                //EtherealCloak
                if (rcRand == 2)
                {
                    descValue1 = "<color=#50C878>15%</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Ethereal Cloak";
                    newSprite = spriteContainer.transform.Find("EtherealCloak").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Reduce dash cooldown by " + descValue1;
                }
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
                rcRand = Random.Range(0, 3);
                //AncientSigil
                if (rcRand == 0)
                {
                    descValue1 = "<color=#50C878>30%</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Ancient Sigil";
                    newSprite = spriteContainer.transform.Find("AncientSigil").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Reduces all weapon cooldowns by ";
                }
                //ChargeStone
                if (rcRand == 1)
                {
                    descValue1 = "<color=#50C878>2</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Charge Stone";
                    newSprite = spriteContainer.transform.Find("ChargeStone").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "After casting an attack, immediately cast it again";
                }
                //HolyEmbrace
                if (rcRand == 2)
                {
                    descValue1 = "<color=#50C878>+1</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Holy Embrace";
                    newSprite = spriteContainer.transform.Find("HolyEmbrace").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Negates damage the first time you enter a room";
                }
                break;
            case "Sacred":
                //
                //
                //
                break;
            default:
                Debug.Log("Improper Card!");
                break;
        }

        if (rc == relicCard1) { RCItem1 = rcTitle.text; }
        if (rc == relicCard2) { RCItem2 = rcTitle.text; }
        if (rc == relicCard3) { RCItem3 = rcTitle.text; }
    }
}
