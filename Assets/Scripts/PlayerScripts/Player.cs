using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Player data
    public float MaxHp;
    public float hp;
    public bool hit = false;
    Color oldColor;
    private IEnumerator hitstate;

    public Image healthbar;

    //Player Immunities
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
    public Sprite manaStaff;
    public Sprite fireStaff;
    public Sprite earthStaff;
    public Sprite waterStaff;
    public Sprite windStaff;

    [Header("SFX")]
    public AudioSource DamageSFX;

    // Use this for initialization
    void Start()
    {
        hp = MaxHp;
        GetWeaponType();
    }

    void Update()
    {
        if(invinTimer > 0)
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
        if (weaponType == 0) { this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = manaStaff; }
        if (weaponType == 1) { this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = fireStaff; }
        if (weaponType == 2) { this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = earthStaff; }
        if (weaponType == 3) { this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = waterStaff; }
        if (weaponType == 4) { this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = windStaff; }
    }

    public void TakeDamage(int amount)
    {
        if(invinTimer <= 0)
        {
            DamageSFX.Play();
            invinTimer = invincibility;
            hp -= amount;
            healthbar.fillAmount = hp / MaxHp;
            if (hp <= 0)
            {
                Debug.Log("You Lose");
            }
        } else if(invinTimer > 0)
        {
            Debug.Log("IMMUNE!!!");
        }
    }
}