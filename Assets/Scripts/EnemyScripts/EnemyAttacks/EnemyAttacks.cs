using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public EnemyMovement enemyMove;
    public Animator animator;

    public float attackTimer1;
    public float timer1;
    public GameObject attackPoint;

    [Header("Attacks")]
    public bool isProjectile;
    public GameObject attackType1;

    void Start()
    {
        enemyMove = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if(enemyMove.inRange == true)
        {
            timer1 += Time.deltaTime;
            if (timer1 >= attackTimer1)
            {
                timer1 = 0;
                //Debug.Log("Attack Type 1");
                animator.SetBool("isAiming", true);
                enemyMove.isAiming = true;
            }
        }
        if(enemyMove.inRange == false)
        {
            timer1 = 0;
        }
    }
    public void disableAim()
    {
        animator.SetBool("isAiming", false);
        enemyMove.isAiming = false;
    }
    public void AttackType1()
    {
        if (isProjectile == true)
        {
            ObjectPooler.Instance.SpawnFromPool("Arrow", attackPoint.transform.position, transform.rotation);
        }
        if(isProjectile == false)
        {
            
        }
    }

}
