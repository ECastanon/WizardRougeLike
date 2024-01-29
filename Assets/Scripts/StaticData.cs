using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===================================================================================
//Contains all data that is needed to be stored and passed between game levels
//===================================================================================
public class StaticData : MonoBehaviour
{
    public static int sceneNumber;
    [Header("RelicIcon Data")]
    public static List<int> relicLevel_ = new List<int>();
    public static List<string> relicStringList_ = new List<string>();
    [Header("Soul Jar Data")]
    public static int EnemyKillsforSoulJar;
    public static int earnedSJHP;
    [Header("EXP Data")]
    public static int earnedEXP;
    [Header("RC Count")]
    public static float esPendant;
    public static float eStone;
    public static float sJar;
    public static float aTalisman;
    public static float cRing;
    public static float Monocle;
    public static float eCloak;
    public static float gSerum;
    public static float hSandals;
    public static float moValor;
    public static float soMight;
    public static float vTooth;
    public static float aSigil;
    public static float cStone;
    public static float hEmbrace;

    [Header("RC Levels")]
    public static int esPendantLvl;
    public static int eStoneLvl;
    public static int oldSJLevel = 0;
    public static int sJarLvl;
    public static int aTalismanLvl;
    public static int cRingLvl;
    public static int MonocleLvl;
    public static int eCloakLvl;
    public static int gSerumLvl;
    public static int hSandalsLvl;
    public static int moValorLvl;
    public static int soMightLvl;
    public static int vToothLvl;
    public static int aSigilLvl;
    public static int cStoneLvl;
    public static int hEmbraceLvl;
}
