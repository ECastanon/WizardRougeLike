using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicEffects : MonoBehaviour
{
    private GameObject player;
    public RelicIcons icons;
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
                aTalisman += 1;
                aTalismanLvl = ((int)aTalisman-1)/3 + 1;
                for(int i = 0; i < icons.relicStringList.Count; i++){if(icons.relicStringList[i] == ("Augur's Talisman")){icons.relicLevel[i] = aTalismanLvl;}}
                //Effect plays in PlayerMovement
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
                player.GetComponent<Player>().GrowthSerum = gSerumLvl;
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
    public void StoreRelicData() //Sends current relic information to StaticData
    {
        StaticData.esPendant = esPendant;
        StaticData.eStone = eStone;
        StaticData.sJar = sJar;
        StaticData.aTalisman = aTalisman;
        StaticData.cRing = cRing;
        StaticData.Monocle = Monocle;
        StaticData.eCloak = eCloak;
        StaticData.gSerum = gSerum;
        StaticData.hSandals = hSandals;
        StaticData.moValor = moValor;
        StaticData.soMight = soMight;
        StaticData.vTooth = vTooth;
        StaticData.aSigil = aSigil;
        StaticData.cStone = cStone;
        StaticData.hEmbrace = hEmbrace;

        StaticData.esPendantLvl = esPendantLvl;
        StaticData.eStoneLvl = eStoneLvl;
        StaticData.sJarLvl = sJarLvl;
        StaticData.aTalismanLvl = aTalismanLvl;
        StaticData.cRingLvl = cRingLvl;
        StaticData.MonocleLvl = MonocleLvl;
        StaticData.eCloakLvl = eCloakLvl;
        StaticData.gSerumLvl = gSerumLvl;
        StaticData.hSandalsLvl = hSandalsLvl;
        StaticData.moValorLvl = moValorLvl;
        StaticData.soMightLvl = soMightLvl;
        StaticData.vToothLvl = vToothLvl;
        StaticData.aSigilLvl = aSigilLvl;
        StaticData.cStoneLvl = cStoneLvl;
        StaticData.hEmbraceLvl = hEmbraceLvl;
    }
    public void LoadRelicData() //Applies relic information from StaticData and reenables any effects
    {
        esPendant = StaticData.esPendant;
        eStone = StaticData.eStone;
        sJar = StaticData.sJar;
        aTalisman = StaticData.aTalisman;
        cRing = StaticData.cRing;
        Monocle = StaticData.Monocle;
        eCloak = StaticData.eCloak;
        gSerum = StaticData.gSerum;
        hSandals = StaticData.hSandals;
        moValor = StaticData.moValor;
        soMight = StaticData.soMight;
        vTooth = StaticData.vTooth;
        aSigil = StaticData.aSigil;
        cStone = StaticData.cStone;
        hEmbrace = StaticData.hEmbrace;

        esPendantLvl = StaticData.esPendantLvl;
        eStoneLvl = StaticData.eStoneLvl;
        sJarLvl = StaticData.sJarLvl;
        aTalismanLvl = StaticData.aTalismanLvl;
        cRingLvl = StaticData.cRingLvl;
        MonocleLvl = StaticData.MonocleLvl;
        eCloakLvl = StaticData.eCloakLvl;
        gSerumLvl = StaticData.gSerumLvl;
        hSandalsLvl = StaticData.hSandalsLvl;
        moValorLvl = StaticData.moValorLvl;
        soMightLvl = StaticData.soMightLvl;
        vToothLvl = StaticData.vToothLvl;
        aSigilLvl = StaticData.aSigilLvl;
        cStoneLvl = StaticData.cStoneLvl;
        hEmbraceLvl = StaticData.hEmbraceLvl;
        
        player = GameObject.FindGameObjectWithTag("Player");
        if(esPendantLvl > 0){player.GetComponent<Player>().EnableSP();}
        if(cRingLvl < 20){player.GetComponent<StaffAttacks>().cooldownStrongCur = player.GetComponent<StaffAttacks>().cooldownStrong * (1 - (cRingLvl * .1f));}
        if(eCloakLvl > 0){player.GetComponent<PlayerMovement>().currentDashCD = (player.GetComponent<PlayerMovement>().dashCD * (1 - (eCloakLvl * .15f)));}
        if(hSandalsLvl > 0){player.GetComponent<PlayerMovement>().currentMoveSpeed = (player.GetComponent<PlayerMovement>().moveSpeed * (1.0f + hSandals * .15f));}
        if(soMightLvl > 0){player.GetComponent<Player>().soMightDamage = soMightLvl;}

    }
}
