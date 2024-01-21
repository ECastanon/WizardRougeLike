using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAttacks : MonoBehaviour
{
    //Phase-1
    //Periodically fires homing attacks
    //Summons small (1-3) groups of skeletons nearby
    //Casts posoinous blasts towards the player that permanently stay on the ground and harm the player

    //Phase-2 <=50%
    //Move speed increases and may start to circle in reverse
    //Homing attacks increase speed
    //Summons larger (3-5) groups of skeletons
    //Occasionally creates a shockwave on the ground that the player must avoid
    public GameObject player;
    [Header("Boss Data")]
    public bool isPhaseTwo = false;
    private NecromancerMovement nm;
    private Animator anim;
    private List<GameObject> pointList = new List<GameObject>();
    private Transform attackPoint;

    [Header("Attack Objects")]
    public GameObject homing;
    public GameObject skeleton;
    public GameObject poison;
    public GameObject shockwave;

    [Header("AttackTimers")]
    public float homingTime;
    public float summonTime;
    public float poisonTime;
    public float shockwaveTime;
    public float timerhoming, timersummon, timerpoison, timershockwave;

    void Start()
    {
        nm = GetComponent<NecromancerMovement>();
        anim = GetComponent<Animator>();
        attackPoint = transform.GetChild(1);
        ResetPointList();
    }

    void Update()
    {
        //Timer Updates
        timerhoming += Time.deltaTime; timersummon += Time.deltaTime;
        if(!isPhaseTwo){timerpoison += Time.deltaTime;} else {timershockwave += Time.deltaTime;}

        if(timerhoming >= homingTime)
        {
            timerhoming = 0;
            HomingAttack();
        }
        if(timersummon >= summonTime)
        {
            timersummon = 0;
            nm.travelSpeed = 0;
            nm.RotateSpeed = 0;
            anim.Play("Necromancer_Summon");
        }
        if(timerpoison >= poisonTime && !isPhaseTwo)
        {
            timerpoison = 0;
            LaunchPoison();
            //
        }
        if(timershockwave >= shockwaveTime && isPhaseTwo)
        {
            timershockwave = 0;
            nm.travelSpeed = 0;
            nm.RotateSpeed = 0;
            //
        }
    }
    public void HomingAttack()
    {
        Instantiate(homing, attackPoint.position, Quaternion.identity);
    }
    public void SummonSkeletons()
    {
        int min = 1; int max = 3;
        if(isPhaseTwo){min = 3; max = 5;}
        int numberToSpawn = Random.Range(min, max+1);

        //Loops through the number of skeletons to spawn
        //Removes the previous spawn location to always spawn in a different location
        for (int i = 0; i < numberToSpawn; i++)
        {
            int spot = Random.Range(0, pointList.Count);
            Instantiate(skeleton, pointList[spot].transform.position, Quaternion.identity);
            pointList.RemoveAt(spot);
        }
        ResetPointList();
    }
    public void LaunchPoison()
    {
        GameObject pBlast = Instantiate(poison, attackPoint.position, Quaternion.identity);
    }
    public void ShockWave()
    {

    }

    public void ResetSpeed()
    {
        nm.RotateSpeed = 20;
        nm.travelSpeed = 3;
        if(isPhaseTwo)
        {
            nm.travelSpeed = 5;
            nm.RotateSpeed = 30;
        }
    }
    public void ResetToIdleAnim()
    {
        anim.Play("Necromancer_Idle");
    }
    private void ResetPointList()
    {
        foreach (Transform point in transform.GetChild(0))
        {
            pointList.Add(point.gameObject);
        }
    }
}
