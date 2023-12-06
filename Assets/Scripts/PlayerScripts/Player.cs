using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject gameManager, gameOverPanel;

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

    // Use this for initialization
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
    }

    void Update()
    {
        dodging = GetComponent<PlayerMovement>().isDashing;

        if (invinTimer > 0)
        {
            invinTimer -= Time.deltaTime;
        }
    }

    //Player flashes while invinTimer > 0
    IEnumerator HitState()
    {
        float flashspeed = 0f;
        while (invincibility > 0 || invinTimer > 0)
        {
            oldColor = gameObject.GetComponent<Renderer>().material.color;
            yield return new WaitForSeconds(.03f - flashspeed);
            gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f);
            yield return new WaitForSeconds(.03f - flashspeed);
            flashspeed += Time.deltaTime;
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
}