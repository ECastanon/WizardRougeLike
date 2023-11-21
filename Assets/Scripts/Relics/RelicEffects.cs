using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicEffects : MonoBehaviour
{
    private GameObject player;
    private RelicIcons icons;
    [HideInInspector] public int oldSJLevel = 0;

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
        icons = GameObject.Find("RelicPanel").GetComponent<RelicIcons>();
    }

    public void ApplyRC(string rcName)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        switch (rcName)
        {
            case "Shock Pendant":
                //====================
                esPendant += 1;
                esPendantLvl = ((int)esPendant-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == "Shock Pendant"){icons.relicLevel[i] = esPendantLvl;}}
                player.GetComponent<Player>().EnableSP();
                //Enables ShockPendant child in Player
                //====================
                break;
            case "Ethereal Stone":
                //====================
                eStone += 1;
                eStoneLvl = ((int)eStone-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Ethereal Stone")){icons.relicLevel[i] = eStoneLvl;}}
                //Effect plays in PlayerMovement
                //====================
                break;
            case "Soul Jar":
                //====================
                sJar += 1;
                oldSJLevel = sJarLvl;
                sJarLvl = ((int)sJar-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Soul Jar")){icons.relicLevel[i] = sJarLvl;}}
                //Effect plays in Enemy
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
                cRingLvl = ((int)cRing-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Cosmo Ring")){icons.relicLevel[i] = cRingLvl;}}
                if(cRingLvl < 20)
                {
                    player.GetComponent<StaffAttacks>().cooldownStrongCur = player.GetComponent<StaffAttacks>().cooldownStrong * (1 - (cRingLvl * .1f));
                }
                //====================
                break;
            case "Monocle":
                //====================
                Monocle += 1;
                MonocleLvl = ((int)Monocle-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Monocle")){icons.relicLevel[i] = MonocleLvl;}}
                //Effect plays in Enemy
                //====================
                break;
            case "Ethereal Cloak":
                //====================
                eCloak += 1;
                eCloakLvl = ((int)eCloak-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Ethereal Cloak")){icons.relicLevel[i] = eCloakLvl;}}
                player.GetComponent<PlayerMovement>().currentDashCD = (player.GetComponent<PlayerMovement>().dashCD * (1 - (eCloakLvl * .15f)));
                //====================
                break;
            case "Growth Serum":
                //====================
                gSerum += 1;
                gSerumLvl = ((int)gSerum-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Growth Serum")){icons.relicLevel[i] = gSerumLvl;}}
                player.GetComponent<Player>().GrowthSerum += 1;
                //====================
                break;
            case "Hermes Sandals":
                //====================
                hSandals += 1;
                hSandalsLvl = ((int)hSandals-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Hermes Sandals")){icons.relicLevel[i] = hSandalsLvl;}}
                player.GetComponent<PlayerMovement>().currentMoveSpeed = (player.GetComponent<PlayerMovement>().moveSpeed * (1.0f + hSandals * .15f));
                //====================
                break;
            case "Medal of Valor":
                //====================
                moValor += 1;
                moValorLvl = ((int)moValor-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Medal of Valor")){icons.relicLevel[i] = moValorLvl;}}
                //Effect plays in EnemyCounter
                //====================
                break;
            case "Scroll of Might":
                //====================
                soMight += 1;
                soMightLvl = ((int)soMight-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Scroll of Might")){icons.relicLevel[i] = soMightLvl;}}
                player.GetComponent<Player>().soMightDamage = soMightLvl;
                //====================
                break;
            case "Vampire Tooth":
                //====================
                vTooth += 1;
                vToothLvl = ((int)vTooth-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Vampire Tooth")){icons.relicLevel[i] = vToothLvl;}}
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
