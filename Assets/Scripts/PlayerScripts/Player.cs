using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject gameManager, gameOverPanel;
    public List<SpriteRenderer> sr = new List<SpriteRenderer>();
    public List<Color> oldColors = new List<Color>();

    //Player data
    public float MaxHp;
    public float currentMaxHP;
    public float hp;
    public bool hit = false;
    Color oldColor;
    private IEnumerator hitstate;

    public Image healthbar;
    public TextMeshProUGUI hpcount;

    //Player Immunities
    public bool dodging;
    public bool holyEmbrace = false;
    public float invincibility; // Player's invincibility after being hit
    float invinTimer; //Player's current invincibility
    //Player Weapons
    [Header("WeaponData")]
    public int weaponType = 0;
        //0 - ManaStaff
        //1 - FireStaff
        //2 - EarthStaff
        //3 - WaterStaff
        //4 - WindStaff
    public GameObject staffChild;
    public Sprite manaStaff;
    public Sprite fireStaff;
    public Sprite earthStaff;
    public Sprite waterStaff;
    public Sprite windStaff;
    [Header("Modifiers")]
    public int GrowthSerum;
    public GameObject manacircle;
    public float mcBoost = 1.5f;
    public int damageMod;
    public int soMightDamage;

    [Header("SFX")]
    public AudioSource DamageSFX;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        healthbar = GameObject.Find("Health").GetComponent<Image>();
        hpcount = GameObject.Find("HPCount").GetComponent<TextMeshProUGUI>();
        manacircle = GameObject.Find("ManaCircle");
        DamageSFX = GameObject.Find("PlayerDamaged").GetComponent<AudioSource>();
        gameOverPanel = GameObject.Find("GameOverPanel");

        currentMaxHP = MaxHp;
        hp = MaxHp;

        hpcount.text = hp.ToString() + "/" + MaxHp.ToString();
        GetWeaponType();

        //Load in Data after Player is Initialized
        gameManager.GetComponent<GameManager>().LoadData();
        gameManager.GetComponent<RelicEffects>().LoadRelicData();
        GameObject.Find("RelicPanel").GetComponent<RelicIcons>().LoadData();

        AddDescendantsWithSprite(transform, sr);
        oldColors.RemoveAt(0); oldColors.RemoveAt(0); oldColors.RemoveAt(0); oldColors.RemoveAt(0);
    }

    void Update()
    {
        dodging = GetComponent<PlayerMovement>().isDashing;
    }

    //Player flashes while invinTimer > 0
    IEnumerator HitState()
    {
        while(invinTimer > 0)
        {
            for (int i = 0; i < sr.Count; i++)
            {
                sr[i].color = new Color(oldColors[i].r, oldColors[i].g, oldColors[i].b);
            }
            yield return new WaitForSeconds(.02f);
            for (int i = 0; i < sr.Count; i++)
            {
                sr[i].color = new Color(0.5f, 0.0f, 0.0f);
            }
            yield return new WaitForSeconds(.02f);
            invinTimer -= Time.deltaTime;
        }
        for (int i = 0; i < sr.Count; i++)
        {
            sr[i].color = new Color(oldColors[i].r, oldColors[i].g, oldColors[i].b);
        }
    }

    void GetWeaponType()
    {
        if(staffChild != null)
        {
            if (weaponType == 0) { staffChild.GetComponent<SpriteRenderer>().sprite = manaStaff; }
            if (weaponType == 1) { staffChild.GetComponent<SpriteRenderer>().sprite = fireStaff; }
            if (weaponType == 2) { staffChild.GetComponent<SpriteRenderer>().sprite = earthStaff; }
            if (weaponType == 3) { staffChild.GetComponent<SpriteRenderer>().sprite = waterStaff; }
            if (weaponType == 4) { staffChild.GetComponent<SpriteRenderer>().sprite = windStaff; }
        }
    }

    public void TakeDamage(int amount)
    {
        if (invinTimer <= 0 && !dodging)
        {
            if (!holyEmbrace)
            {
                DamageSFX.Play();
                invinTimer = invincibility;
                StartCoroutine(HitState());
                hp -= amount;
                UpdateHPBar();
                if (hp <= 0)
                {
                    gameOverPanel.GetComponent<Animator>().Play("GameOverSlideIn");
                    GameObject[] ea = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject enemy in ea)
                    {
                        enemy.GetComponent<EnemyActivator>().Deactivate();
                    }
                    Time.timeScale = 0;
                }
            } else
            {
                //Play deflection sound
                //disable holyembrace
                holyEmbrace = false;
                Debug.Log("Holy Embrace!");
            }
        } else if(invinTimer > 0)
        {
            Debug.Log("IMMUNE!!!");
        }
    }

    public void UpdateHPBar()
    {
        healthbar.fillAmount = hp / currentMaxHP;
        hpcount.text = hp.ToString() + "/" + currentMaxHP.ToString();
    }

    public void EnableSP()
    {
        GameObject sp = transform.GetChild(1).gameObject;
        if(!sp.activeSelf)
        {
            sp.SetActive(true);
        }

        sp.GetComponent<ShockPendant>().damage = 5 + ((gameManager.GetComponent<RelicEffects>().esPendantLvl-1) * 2);
        if(gameManager.GetComponent<RelicEffects>().esPendantLvl == 0)
        {
            sp.GetComponent<ShockPendant>().damage = 5;
        }
    }

    public void EnableManaCircle()
    {
        manacircle.GetComponent<ManaCircle>().player = this.gameObject;
        manacircle.GetComponent<ManaCircle>().Enable();
        manacircle.transform.position = transform.position;
    }
    private void AddDescendantsWithSprite(Transform parent, List<SpriteRenderer> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.GetComponent<SpriteRenderer>() != null && child.gameObject.name != "Shadow")
            {
                list.Add(child.gameObject.GetComponent<SpriteRenderer>());
                oldColors.Add(child.gameObject.GetComponent<SpriteRenderer>().color);
            }
            AddDescendantsWithSprite(child, list);
        }
    }
}