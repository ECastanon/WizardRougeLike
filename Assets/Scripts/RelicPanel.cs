using UnityEngine;
using UnityEngine.UI;

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
    public GameObject rctitle1;
    public GameObject rctitle2;
    public GameObject rctitle3;
    //Sprite
    public GameObject rcsprite1;
    public GameObject rcsprite2;
    public GameObject rcsprite3;
    //Description
    public GameObject rcdesc1;
    public GameObject rcdesc2;
    public GameObject rcdesc3;

    private int temp = 1;

    private bool inView = false;

    public string roomType;

    [Header("Uncommon")]
    public GameObject etherealStone; //Increases dash distance by 1.5x
    public GameObject etherealShockPendant; //Damages enemies dodged into
    public GameObject soulJar; //Increases maxHP by 1 for every 5 enemies defeated

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
        GenerateRelic(relicCard1);
        GenerateRelic(relicCard2);
        GenerateRelic(relicCard3);

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

    public void GenerateRelic(GameObject rc)
    {
        float rand = Random.Range(0.0f, 1.0f);
        temp += 1;
        string rarity = "";

        if (roomType == "Basic" || roomType == "Untagged")
        {
            if (rand <= .05f) //5%
            {
                //Create an Super Relic
                rarity = "Super";
                ChangeRCBorderColor(rc, rarity);
            }
            else if (.05f < rand && rand <= .20f) //15%
            {
                //Create a Rare Relic
                rarity = "Rare";
                ChangeRCBorderColor(rc, rarity);
            }
            else if (.20f < rand && rand <= .50f) //30%
            {
                //Create a Uncommon Relic
                rarity = "Uncommon";
                ChangeRCBorderColor(rc, rarity);
            }
            else if (.50f > rand) //50%
            {
                //Create a Common Relic
                rarity = "Common";
                ChangeRCBorderColor(rc, rarity);
            }
        }
        if (roomType == "TreasureRoom")
        {
            if(rand >= .8f) //20%
            {
                //Create an Ultra Relic
                rarity = "Ultra";
                ChangeRCBorderColor(rc, rarity);
            }
            else //80%
            {
                //Create a Super Relic
                rarity = "Super";
                ChangeRCBorderColor(rc, rarity);
            }
        }

        Debug.Log("RC" + temp + " = " + rand);
        Debug.Log("My rarity is: " + rarity);
    }

    private void ChangeRCBorderColor(GameObject rc, string rarity)
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
    private void GetRC(GameObject rc, string rarity)
    {
        //Returns 0-99
        //int rand = Random.Range(0, 100);
        int rand = Random.Range(0, 4);
        switch (rarity)
        {
            case "Common":
                //
                break;
            case "Uncommon":
                //
                break;
            case "Rare":
                //
                break;
            case "Super":
                //
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
