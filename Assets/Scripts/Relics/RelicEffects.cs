using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicEffects : MonoBehaviour
{
    private GameObject player;
    [HideInInspector]
    public int oldSJLevel = 0;

    [Header("RC Count")]
    public float esPendant;
    public float eStone;
    public float sJar;
    public float aTalisman;
    public float cRing;
    public float Monocle;
    public float eCloak;
    public float gSerum;
    public float hSandals;
    public float moValor;
    public float soMight;
    public float vTooth;
    public float aSigil;
    public float cStone;
    public float hEmbrace;

        [Header("RC Levels")]
    public int esPendantLvl;
    public int eStoneLvl;
    public int sJarLvl;
    public int aTalismanLvl;
    public int cRingLvl;
    public int MonocleLvl;
    public int eCloakLvl;
    public int gSerumLvl;
    public int hSandalsLvl;
    public int moValorLvl;
    public int soMightLvl;
    public int vToothLvl;
    public int aSigilLvl;
    public int cStoneLvl;
    public int hEmbraceLvl;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ApplyRC(string rcName)
    {
        switch (rcName)
        {
            case "Shock Pendant":
                //====================
                esPendant += 1;
                player.GetComponent<Player>().EnableSP();
                //Enables ShockPendant child in Player
                esPendantLvl = ((int)esPendant-1)/3 + 1;
                //====================
                break;
            case "Ethereal Stone":
                //====================
                eStone += 1;
                //Effect plays in PlayerMovement
                eStoneLvl = ((int)eStone-1)/3 + 1;
                //====================
                break;
            case "Soul Jar":
                //====================
                sJar += 1;
                //Effect plays in Enemy
                oldSJLevel = sJarLvl;
                sJarLvl = ((int)sJar-1)/3 + 1;
                //====================
                break;
            case "Augur's Talisman":
                //====================
                //ADD ABILITY
                //====================
                break;
            case "Cosmo Ring":
                //====================
                cRing += 1;
                if(cRing < 20)
                {
                    player.GetComponent<StaffAttacks>().cooldownStrongCur = player.GetComponent<StaffAttacks>().cooldownStrong * (1 - (cRing * .05f));
                }
                //====================
                break;
            case "Monocle":
                //====================
                Monocle += 1;
                //Effect plays in Enemy
                //====================
                break;
            case "Ethereal Cloak":
                //====================
                eCloak += 1;
                player.GetComponent<PlayerMovement>().currentDashCD = (player.GetComponent<PlayerMovement>().dashCD * (1 - (eCloak * .15f)));
                //====================
                break;
            case "Growth Serum":
                //====================
                gSerum += 1;
                player.GetComponent<Player>().GrowthSerum += 1;
                //====================
                break;
            case "Hermes Sandals":
                //====================
                hSandals += 1;
                player.GetComponent<PlayerMovement>().currentMoveSpeed = (player.GetComponent<PlayerMovement>().moveSpeed * (1.0f + hSandals * .15f));
                //====================
                break;
            case "Medal of Valor":
                //====================
                moValor += 1;
                //Effect plays in EnemyCounter
                //====================
                break;
            case "Scroll of Might":
                //====================
                soMight += 1;
                player.GetComponent<Player>().damageMod += 1;
                //====================
                break;
            case "Vampire Tooth":
                //====================
                vTooth += 1;
                //Effect plays in Enemy
                //====================
                break;
            case "Ancient Sigil":
                //====================
                //ADD ABILITY
                //====================
                break;
            case "Charge Stone":
                //====================
                cStone += 1;
                //Effect plays in StaffAttacks
                //====================
                break;
            case "Holy Embrace":
                //====================
                hEmbrace += 1;
                //Effect is enabled in RoomData and plays in Player
                //====================
                break;


            default:
                Debug.Log("Improper RC Name");
                break;
        }
    }
}
