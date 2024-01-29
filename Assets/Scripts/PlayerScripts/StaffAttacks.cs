using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffAttacks : MonoBehaviour
{
    public Player player;
    private GameObject gameManager;
    public GameObject staffTip;
    private int currentStaff;

    public bool ManaCircle = false;

    public float cooldownBasic;
    public float cooldownStrong;
    public float cooldownStrongCur;
    public float cooldownCharge;
    public float cooldownChargeCur;
    public float timerBasic = 0;
    public float timerStrong = 0;
    public float timerCharge = 0;

    private Image strongBar, strongIcon;
    private Image chargeBar, chargeIcon;

    private float chargeStoneDelay = .25f;

    ObjectPooler objectPooler;
    private AudioSource BasicAttack, StrongAttack, ChargeAttack;

    void Start()
    {
        player = GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        strongBar = GameObject.Find("strongBar").GetComponent<Image>();
        strongIcon = GameObject.Find("strongIcon").GetComponent<Image>();
        chargeBar = GameObject.Find("chargeBar").GetComponent<Image>();
        chargeIcon = GameObject.Find("chargeIcon").GetComponent<Image>();
        objectPooler = ObjectPooler.Instance;
        currentStaff = player.weaponType;
        SetCooldowns();
    }

    void Update()
    {
        if (!PauseMenu.paused)
        {
            if (cooldownBasic >= timerBasic) { timerBasic = timerBasic + Time.deltaTime; }
            if (cooldownStrongCur >= timerStrong) { timerStrong = timerStrong + Time.deltaTime; }
            if (cooldownChargeCur >= timerCharge) { timerCharge = timerCharge + Time.deltaTime; }

            ManaStaff();

            strongBar.fillAmount = 1 - (timerStrong / cooldownStrongCur);
            chargeBar.fillAmount = 1 - (timerCharge / cooldownChargeCur);
        }
    }

    public void SetCooldowns()
    {
        if(currentStaff == 0)
        {
            cooldownBasic = 1f;
            cooldownStrong = 3f;
            cooldownStrongCur = cooldownStrong;
            cooldownCharge = 10f;
            cooldownChargeCur = cooldownCharge;

            timerBasic = cooldownBasic;
            timerStrong = cooldownStrongCur;
            timerCharge = cooldownChargeCur;

            BasicAttack = GameObject.Find("MagicMissileSFX").GetComponent<AudioSource>();
            StrongAttack = GameObject.Find("ManaBlastSFX").GetComponent<AudioSource>();
            ChargeAttack = GameObject.Find("MagicCircleSFX").GetComponent<AudioSource>();
        }
    }


    IEnumerator ChargeStoneAttack(string objToSpawn)
    {
        yield return new WaitForSeconds(chargeStoneDelay);
        //ONLY USED IF THE CHARGE STONE IS ACTIVE
        if (gameManager.GetComponent<RelicEffects>().cStone > 0)
        {
            objectPooler.SpawnFromPool(objToSpawn, staffTip.transform.position, transform.rotation);
        }
    }

    public void ManaStaff()
    {
        string objToSpawn;
        if (UserInput.instance.BasicAttackInput)
        {
            objToSpawn = "ManaMissile";
            if(timerBasic >= cooldownBasic)
            {
                timerBasic = 0;
                objectPooler.SpawnFromPool(objToSpawn, staffTip.transform.position, transform.rotation);
                StartCoroutine(ChargeStoneAttack(objToSpawn));
                BasicAttack.Play();
            }
        }
        if (UserInput.instance.HeavyAttackInput)
        {
            objToSpawn = "ManaBlast";
            if (timerStrong >= cooldownStrongCur)
            {
                timerStrong = 0;
                objectPooler.SpawnFromPool(objToSpawn, staffTip.transform.position, transform.rotation);
                StartCoroutine(ChargeStoneAttack(objToSpawn));
                StrongAttack.Play();
            }

        }
        if (UserInput.instance.ChargeAttackInput)
        {
            objToSpawn = "CircleOfMana";
            if (timerCharge >= cooldownCharge)
            {
                timerCharge = 0;
                player.EnableManaCircle();
                ChargeAttack.Play();
            }
        }
    }
}
