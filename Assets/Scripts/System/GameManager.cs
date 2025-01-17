using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Soul Jar Data")]
    public int EnemyKillsforSoulJar;
    public int earnedSJHP;

    [Header("Medal of Valor Data")]
    public int healBy = 2;

    [Header("EXP Data")]
    public TextMeshProUGUI expText;
    //Total player EXP
    public int totalEXP;
    //EXP earned within the current run
    //Added to totalEXP after the run
    public int earnedEXP;
    private int combinedEXP;

    void Update()
    {
        combinedEXP = earnedEXP + totalEXP;
        expText.GetComponent<TextMeshProUGUI>().text = "EXP: " + combinedEXP;
    }

    public void StoreData() //Sends current information to StaticData
    {
        StaticData.earnedEXP = earnedEXP;
        StaticData.EnemyKillsforSoulJar = EnemyKillsforSoulJar;
        StaticData.earnedSJHP = earnedSJHP;
    }
    public void LoadData() //Applies information from StaticData
    {
        earnedEXP = StaticData.earnedEXP;
        EnemyKillsforSoulJar = StaticData.EnemyKillsforSoulJar;
        earnedSJHP = StaticData.earnedSJHP;
    }
}
