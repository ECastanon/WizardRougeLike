using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Soul Jar Data")]
    public int EnemyKillsforSoulJar;
    public int soulJarStacks;

    [Header("Medal of Valor Data")]
    public int healBy = 2;

    [Header("Vampire Tooth")]
    public int toothHeal = 1;

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
}
