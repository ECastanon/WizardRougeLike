using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAttacks : MonoBehaviour
{
    //Move speed increases and may start to circle in reverse
    //Homing attacks increase speed
    //Summons larger (3-5) groups of skeletons
    //Occasionally creates a shockwave on the ground that the player must avoid
    public GameObject player;
    [Header("Boss Data")]
    public bool isPhaseTwo = false;
    public Transform SpawnParent;
    private NecromancerMovement nm;
    private Animator anim;
    private bool isAnimState = false;
    [SerializeField] private List<GameObject> pointList = new List<GameObject>();
    private Transform attackPoint;
    private AudioSource SummonSFX, ShockWaveSFX, PoisonBlastSFX, DeathSFX;
    [SerializeField] private GameObject BlockerContainer;
    public int hitCounter = 0;

    [Header("Attack Objects")]
    public GameObject homing;
    public GameObject skeleton;
    public GameObject poison;
    public GameObject shockwave;
    public GameObject BoneBlocker;

    [Header("AttackTimers")]
    public float homingTime;
    public float summonTime;
    public float poisonTime;
    public float shockwaveTime;
    private float timerhoming, timersummon, timerpoison, timershockwave;

    void Start()
    {
        nm = GetComponent<NecromancerMovement>();
        anim = GetComponent<Animator>();
        attackPoint = transform.GetChild(1);
        SummonSFX = GameObject.Find("Summon").GetComponent<AudioSource>();
        ShockWaveSFX = GameObject.Find("ShockwaveSFX").GetComponent<AudioSource>();
        PoisonBlastSFX = GameObject.Find("PoisonBlastSFX").GetComponent<AudioSource>();
        DeathSFX = GameObject.Find("DeathSFX").GetComponent<AudioSource>();
        ResetPointList();
    }

    void Update()
    {
        //Timer Updates
        timerhoming += Time.deltaTime; timersummon += Time.deltaTime;
        if(!isPhaseTwo){timerpoison += Time.deltaTime;} else {timershockwave += Time.deltaTime;}
        //Some attacks can only be conducted is the boss is not in an animation
        if(timerhoming >= homingTime)
        {
            timerhoming = 0;
            HomingStrike();
        }
        if(timersummon >= summonTime && !isAnimState)
        {
            timersummon = 0;
            ZeroSpeed();
            anim.Play("Necromancer_Summon");
        }
        if(timerpoison >= poisonTime && !isPhaseTwo)
        {
            timerpoison = 0;
            LaunchPoison();
            //
        }
        if(timershockwave >= shockwaveTime && isPhaseTwo && !isAnimState)
        {
            timershockwave = 0;
            ZeroSpeed();
            anim.Play("Necromancer_Shockwave");
        }

        BlockerContainer.transform.localEulerAngles += new Vector3(0,0, 5 * Time.deltaTime);
    }
    public void HomingStrike()
    {
        GameObject attack = Instantiate(homing, attackPoint.position, Quaternion.identity);
        attack.transform.SetParent(SpawnParent);
        if(isPhaseTwo){attack.GetComponent<HomingAttack>().spd = 5;}
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
            GameObject skele = Instantiate(skeleton, pointList[spot].transform.position, Quaternion.identity);
            skele.transform.SetParent(SpawnParent.GetChild(0));
            skele.GetComponent<EnemyMovement>().range = 15;
            skele.GetComponent<Enemy>().experience = 0;
            skele.GetComponent<Enemy>().noDeathRelics = true;
            pointList.RemoveAt(spot);
        }
        SummonSFX.Play();
        ResetPointList();
    }
    public void LaunchPoison()
    {
        PoisonBlastSFX.Play();
        GameObject pBlast = Instantiate(poison, attackPoint.position, Quaternion.identity);
        pBlast.transform.SetParent(SpawnParent);
    }
    public void ShockWave()
    {
        ShockWaveSFX.Play();
        GameObject s1 = Instantiate(shockwave, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        GameObject s2 = Instantiate(shockwave, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        GameObject s3 = Instantiate(shockwave, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
        GameObject s4 = Instantiate(shockwave, transform.position, Quaternion.Euler(new Vector3(0, 0, 270)));
        GameObject s5 = Instantiate(shockwave, transform.position, Quaternion.Euler(new Vector3(0, 0, 45)));
        GameObject s6 = Instantiate(shockwave, transform.position, Quaternion.Euler(new Vector3(0, 0, 135)));
        GameObject s7 = Instantiate(shockwave, transform.position, Quaternion.Euler(new Vector3(0, 0, 225)));
        GameObject s8 = Instantiate(shockwave, transform.position, Quaternion.Euler(new Vector3(0, 0, 315)));
        s1.transform.SetParent(SpawnParent);s2.transform.SetParent(SpawnParent);s3.transform.SetParent(SpawnParent);
        s4.transform.SetParent(SpawnParent);s5.transform.SetParent(SpawnParent);s6.transform.SetParent(SpawnParent);
        s7.transform.SetParent(SpawnParent);s8.transform.SetParent(SpawnParent);
    }
    public void CreateBlockers()
    {
        if(hitCounter % 3 == 0)
        {
            BlockerContainer.transform.localEulerAngles = new Vector3(0,0,0);
            foreach(Transform child in BlockerContainer.transform)
            {
                Destroy(child.gameObject);
            }

            //Loops through the number of blockers to spawn
            //Removes the previous spawn location to always spawn in a different location
            for (int i = 0; i < 4; i++)
            {
                int spot = Random.Range(0, pointList.Count);
                GameObject blocker = Instantiate(BoneBlocker, pointList[i].transform.position, Quaternion.identity);
                blocker.transform.SetParent(BlockerContainer.transform);
                pointList.RemoveAt(spot);
            }
            ResetPointList();
        }
    }
    public void ZeroSpeed()
    {
        nm.travelSpeed = 0;
        nm.RotateSpeed = 0;
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
    public void AnimState()
    {
        isAnimState = !isAnimState;
    }
    public void StartPhaseTwo()
    {
        isPhaseTwo = true;
        nm.travelSpeed = 5;
        nm.RotateSpeed = 30;
    }
    public void DeathAnim()
    {
        timerhoming = 0;timerpoison = 0;timersummon = 0;timershockwave = 0;
        anim.Play("Necromancer_Death");
        foreach(Transform child in SpawnParent)
        {
            Destroy(child.gameObject);
        }
        SpawnParent.GetChild(0).gameObject.GetComponent<EnemyCounter>().Enemies.Clear();
    }
    public void DeathSound()
    {
        DeathSFX.Play();
    }
}
