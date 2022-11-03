using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAttacks : MonoBehaviour
{
    public Player player;
    public GameObject staffTip;
    private int currentStaff;

    public float cooldownBasic;
    public float cooldownStrong;
    public float timerBasic = 0;
    public float timerStrong = 0;

    ObjectPooler objectPooler;

    void Start()
    {
        player = GetComponent<Player>();
        objectPooler = ObjectPooler.Instance;
        currentStaff = player.weaponType;
        SetCooldowns();
    }

    void Update()
    {
        if (!PauseMenu.paused)
        {
            if (cooldownBasic >= timerBasic) { timerBasic = timerBasic + Time.deltaTime; }
            if (cooldownStrong >= timerStrong) { timerStrong = timerStrong + Time.deltaTime; }
            ManaStaff();
        }
    }

    public void SetCooldowns()
    {
        if(currentStaff == 0)
        {
            cooldownBasic = 1f;
            cooldownStrong = 3f;
        }
    }

    public void ManaStaff()
    {
        if (Input.GetMouseButton(0))
        {
            if(timerBasic >= cooldownBasic)
            {
                timerBasic = 0;
                objectPooler.SpawnFromPool("ManaMissile", staffTip.transform.position, transform.rotation);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (timerStrong >= cooldownStrong)
            {
                timerStrong = 0;
                objectPooler.SpawnFromPool("ManaBlast", staffTip.transform.position, transform.rotation);
            }
        }
    }
}
