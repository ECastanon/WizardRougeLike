using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RelicPanel : MonoBehaviour
{
    private Animator anim;
    private GameObject gameManager;
    public GameObject RCFacePanel;
    private RelicIcons relicIcons;
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
    //Buttons
    public List<GameObject> rcButtonList = new List<GameObject>();

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

    [Header("Reroll and Eliminate")]
    public TextMeshProUGUI rerollText;
    public int rerollCount;

    [HideInInspector]
    public string roomType;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        RCFacePanel = GameObject.Find("rcfacePanel");
        relicIcons = GetComponent<RelicIcons>();
        rcButtonList.Add(GameObject.Find("RCButton1"));rcButtonList.Add(GameObject.Find("RCButton2"));rcButtonList.Add(GameObject.Find("RCButton3"));
    }

    //Called In EnemyCounter
    public void SlideInMenu()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<StaffAttacks>().enabled = false;
        rerollText.GetComponent<TextMeshProUGUI>().text = "Reroll (" + rerollCount + ")";

        GenerateRelicAllRelicCards();

        if (!inView)
        {
            anim.Play("RelicPanelSlideIn");
            RCFacePanel.GetComponent<Animator>().Play("RelicPanelSlideIn");
            inView = true;
            Time.timeScale = 0f;
        }
    }
    private void SlideOutMenu()
    {
        if (inView)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<StaffAttacks>().enabled = true;
            anim.Play("RelicPanelSlideOut");
            RCFacePanel.GetComponent<Animator>().Play("RelicPanelSlideOut");
            inView = false;
            Time.timeScale = 1f;
        }
    }

    public void SelectRC1()
    {
        Debug.Log("Player got " + RCItem1);
        SlideOutMenu();
        relicIcons.UpdateIconMenu(RCItem1);
        gameManager.GetComponent<RelicEffects>().ApplyRC(RCItem1);
    }
    public void SelectRC2()
    {
        Debug.Log("Player got " + RCItem2);
        SlideOutMenu();
        relicIcons.UpdateIconMenu(RCItem2);
        gameManager.GetComponent<RelicEffects>().ApplyRC(RCItem2);
    }
    public void SelectRC3()
    {
        Debug.Log("Player got " + RCItem3);
        SlideOutMenu();
        relicIcons.UpdateIconMenu(RCItem3);
        gameManager.GetComponent<RelicEffects>().ApplyRC(RCItem3);
    }

    //Button for rerolling the given relics
    public void ReRoll()
    {
        if(rerollCount > 0)
        {
            GenerateRelicAllRelicCards();

            rerollCount--;
            rerollText.GetComponent<TextMeshProUGUI>().text = "Reroll (" + rerollCount + ")";
        }
    }
    //Button for deleting a relic for the rest of the run
    public void Eliminate()
    {
        
    }

    //Generates all 3 Relics and prevents duplicates
    public void GenerateRelicAllRelicCards()
    {
        GenerateRelic(relicCard1, rctitle1, rcsprite1, rcdesc1);
        GenerateRelic(relicCard2, rctitle2, rcsprite2, rcdesc2);
        GenerateRelic(relicCard3, rctitle3, rcsprite3, rcdesc3);

        CheckForMatches();
    }
    public void CheckForMatches()
    {
        //Improve matching check by removing the matched relic from a reroll
        if(RCItem1 == RCItem2){ GenerateRelic(relicCard1, rctitle1, rcsprite1, rcdesc1); CheckForMatches();}
        if(RCItem1 == RCItem3){ GenerateRelic(relicCard1, rctitle1, rcsprite1, rcdesc1); CheckForMatches();}
        if(RCItem2 == RCItem3){ GenerateRelic(relicCard2, rctitle2, rcsprite2, rcdesc2); CheckForMatches();}
        RCNum = 1;
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
                border = rc.transform.GetChild(0).gameObject;
                border.GetComponent<Image>().color = new Color(.717f, .717f, .717f);
                break;
            //Change Border Color to Green
            case "Uncommon":
                border = rc.transform.GetChild(0).gameObject;
                border.GetComponent<Image>().color = new Color(.305f, .945f, .235f);
                break;
            //Change Border Color to Blue
            case "Rare":
                border = rc.transform.GetChild(0).gameObject;
                border.GetComponent<Image>().color = new Color(.058f, .521f, 1);
                break;
            //Change Border Color to Red
            case "Super":
                border = rc.transform.GetChild(0).gameObject;
                border.GetComponent<Image>().color = new Color(.913f, .098f, 0);

                break;
            //Change Border Color to Purple
            case "Ultra":
                border = rc.transform.GetChild(0).gameObject;
                border.GetComponent<Image>().color = new Color(.627f, 0, .905f);

                break;
            //Change Border Color to Yellow
            case "Sacred":
                border = rc.transform.GetChild(0).gameObject;
                border.GetComponent<Image>().color = new Color(1, .835f, .2f);
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
        switch (rarity)
        {
            case "Common":
                rcRand = Random.Range(0, 3);
                //EtherealStone
                if(rcRand == 0)
                {
                    float Value1 = 10 * (gameManager.GetComponent<RelicEffects>().eStoneLvl + 1);
                    descValue1 = "<color=#50C878>" + Value1 + "%</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Ethereal Stone";
                    newSprite = spriteContainer.transform.Find("Ethereal Stone").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;

                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().eStoneLvl) - (gameManager.GetComponent<RelicEffects>().eStone-1);
                    if(gameManager.GetComponent<RelicEffects>().eStone == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Increases dash distance by " + descValue1;
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                //ShockPendant
                if (rcRand == 1)
                {
                    float Value1 = 5 + (gameManager.GetComponent<RelicEffects>().esPendantLvl * 2);
                    if(gameManager.GetComponent<RelicEffects>().esPendantLvl == 0){Value1 = 5;}
                    descValue1 = "<color=#50C878>" + Value1 + "</color>";
                    
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Shock Pendant";
                    newSprite = spriteContainer.transform.Find("Shock Pendant").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                
                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().esPendantLvl) - (gameManager.GetComponent<RelicEffects>().esPendant-1);
                    if(gameManager.GetComponent<RelicEffects>().esPendant == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Creates a bolt of lightning that deals " + descValue1 + " damage to a random enemy";
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                //SoulJar
                if (rcRand == 2)
                {
                    float Value1 = 1 + (gameManager.GetComponent<RelicEffects>().sJarLvl);
                    descValue1 = "<color=#50C878>" + Value1 + "</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Soul Jar";
                    newSprite = spriteContainer.transform.Find("Soul Jar").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                
                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().sJarLvl) - (gameManager.GetComponent<RelicEffects>().sJar-1);
                    if(gameManager.GetComponent<RelicEffects>().sJar == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Increases maxHP by " + descValue1 + " for every 5 enemies defeated";
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                break;
            case "Uncommon":
                rcRand = Random.Range(0, 3);
                //Augur'sTalisman
                if (rcRand == 0)
                {
                    float Value1 = 15 * (gameManager.GetComponent<RelicEffects>().aTalismanLvl + 1);
                    descValue1 = "<color=#50C878>" + Value1 + "%</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Augur's Talisman";
                    newSprite = spriteContainer.transform.Find("Augur's Talisman").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    
                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().aTalismanLvl) - (gameManager.GetComponent<RelicEffects>().aTalisman-1);
                    if(gameManager.GetComponent<RelicEffects>().aTalisman == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Shortens negative status effects by " + descValue1;
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                //CosmoRing
                if (rcRand == 1)
                {
                    float Value1 = 10 * (gameManager.GetComponent<RelicEffects>().cRingLvl + 1);
                    descValue1 = "<color=#50C878>" + Value1 + "%</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Cosmo Ring";
                    newSprite = spriteContainer.transform.Find("Cosmo Ring").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;

                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().cRingLvl) - (gameManager.GetComponent<RelicEffects>().cRing-1);
                    if(gameManager.GetComponent<RelicEffects>().cRing == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Reduces strong attack Cooldowns by " + descValue1;
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                //Monocle
                if (rcRand == 2)
                {
                    float Value1 = 5 * (gameManager.GetComponent<RelicEffects>().MonocleLvl + 1);
                    descValue1 = "<color=#50C878>" + Value1 + "%</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Monocle";
                    newSprite = spriteContainer.transform.Find("Monocle").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;

                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().MonocleLvl) - (gameManager.GetComponent<RelicEffects>().Monocle-1);
                    if(gameManager.GetComponent<RelicEffects>().Monocle == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Increase EXP gain by " + descValue1;
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                break;
            case "Rare":
                rcRand = Random.Range(0, 3);
                //GrowthSerum
                if (rcRand == 0)
                {
                    float Value1 = 10 * (gameManager.GetComponent<RelicEffects>().gSerumLvl + 1);
                    descValue1 = "<color=#50C878>" + Value1 + "%</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Growth Serum";
                    newSprite = spriteContainer.transform.Find("Growth Serum").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;

                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().gSerumLvl) - (gameManager.GetComponent<RelicEffects>().gSerum-1);
                    if(gameManager.GetComponent<RelicEffects>().gSerum == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Increases spell size by " + descValue1;
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                //HermesSandals
                if (rcRand == 1)
                {
                    float Value1 = 15 * (gameManager.GetComponent<RelicEffects>().hSandalsLvl + 1);
                    descValue1 = "<color=#50C878>" + Value1 + "%</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Hermes Sandals";
                    newSprite = spriteContainer.transform.Find("Hermes Sandals").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;

                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().hSandalsLvl) - (gameManager.GetComponent<RelicEffects>().hSandals-1);
                    if(gameManager.GetComponent<RelicEffects>().hSandals == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Increase movement speed by " + descValue1;
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                //EtherealCloak
                if (rcRand == 2)
                {
                    float Value1 = 15 * (gameManager.GetComponent<RelicEffects>().eCloakLvl + 1);
                    descValue1 = "<color=#50C878>" + Value1 + "%</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Ethereal Cloak";
                    newSprite = spriteContainer.transform.Find("Ethereal Cloak").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;

                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().eCloakLvl) - (gameManager.GetComponent<RelicEffects>().eCloak-1);
                    if(gameManager.GetComponent<RelicEffects>().eCloak == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Reduce dash cooldown by " + descValue1;
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                break;
            case "Super":
                rcRand = Random.Range(0, 3);
                //VampireTooth
                if (rcRand == 0)
                {
                    float Value1 = 1 * (gameManager.GetComponent<RelicEffects>().vToothLvl + 1);
                    descValue1 = "<color=#50C878>" + Value1 + "</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Vampire Tooth";
                    newSprite = spriteContainer.transform.Find("Vampire Tooth").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;

                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().vToothLvl) - (gameManager.GetComponent<RelicEffects>().vTooth-1);
                    if(gameManager.GetComponent<RelicEffects>().vTooth == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "30% chance to recover HP by " + descValue1 + " after defeating an enemy";
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                //MedalofValor
                if (rcRand == 1)
                {
                    float Value1 = 2 * (gameManager.GetComponent<RelicEffects>().moValorLvl + 1);
                    descValue1 = "<color=#50C878>" + Value1 + "</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Medal of Valor";
                    newSprite = spriteContainer.transform.Find("Medal of Valor").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;

                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().moValorLvl) - (gameManager.GetComponent<RelicEffects>().moValor-1);
                    if(gameManager.GetComponent<RelicEffects>().moValor == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Recover " + descValue1 + " HP after clearing a room";
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                //ScrollofMight
                if (rcRand == 2)
                {
                    float Value1 = 1 * (gameManager.GetComponent<RelicEffects>().soMightLvl + 1);
                    descValue1 = "<color=#50C878>+" + Value1 + "</color>";

                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Scroll of Might";
                    newSprite = spriteContainer.transform.Find("Scroll of Might").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;

                    float remainingRC;
                    string remainingString;
                    remainingRC = 3 * (gameManager.GetComponent<RelicEffects>().soMightLvl) - (gameManager.GetComponent<RelicEffects>().soMight-1);
                    if(gameManager.GetComponent<RelicEffects>().moValor == 0 || remainingRC == 1)
                    {
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Increase all weapon damage by " + descValue1;
                    } else {
                        remainingString = ("<color=#50C878>" + remainingRC + "</color>").ToString();
                        rcDesc.GetComponent<TextMeshProUGUI>().text = "Collect <color=#50C878>" + remainingRC + "</color> more to level up!";
                    }
                }
                break;
            case "Ultra":
                rcRand = Random.Range(0, 3);
                //AncientSigil
                if (rcRand == 0)
                {
                    //===========================
                    //Update When Effect is added
                    //===========================
                    descValue1 = "<color=#50C878>30%</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Ancient Sigil";
                    newSprite = spriteContainer.transform.Find("Ancient Sigil").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "Reduces all weapon cooldowns by ";
                }
                //ChargeStone
                if (rcRand == 1)
                {
                    //===========================
                    //Update When Effect is added
                    //===========================
                    descValue1 = "<color=#50C878>2</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Charge Stone";
                    newSprite = spriteContainer.transform.Find("Charge Stone").gameObject.GetComponent<SpriteRenderer>();
                    rcSprite.GetComponent<SpriteRenderer>().sprite = newSprite.sprite;
                    rcDesc.GetComponent<TextMeshProUGUI>().text = "After casting an attack, immediately cast it again";
                }
                //HolyEmbrace
                if (rcRand == 2)
                {
                    //===========================
                    //Update When Effect is added
                    //===========================
                    descValue1 = "<color=#50C878>+1</color>";
                    rcTitle.GetComponent<TextMeshProUGUI>().text = "Holy Embrace";
                    newSprite = spriteContainer.transform.Find("Holy Embrace").gameObject.GetComponent<SpriteRenderer>();
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
