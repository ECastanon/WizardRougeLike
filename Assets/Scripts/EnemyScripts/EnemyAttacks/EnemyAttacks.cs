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
    public bool isMelee;
    public bool isRanged;
    public bool isMagic;
    public string ProjectileName;

    void Start()
    {
        enemyMove = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if (enemyMove.inRange == true)
        {
            timer1 += Time.deltaTime;
            if (timer1 >= attackTimer1)
            {
                timer1 = 0;
                //Debug.Log("Attack Type 1");
                if(isMelee){animator.SetBool("Attack", true);}
                if(isRanged){animator.SetBool("RangedAttack", true);}
                if(isMagic){animator.SetBool("MagicAttack", true);}
                enemyMove.isAiming = true;
            }
        }
        if (enemyMove.inRange == false)
        {
            timer1 = 0;
        }
    }
    public void disableAim()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("RangedAttack", false);
        animator.SetBool("MagicAttack", false);
        enemyMove.isAiming = false;
    }
    public void AttackType1()
    {
        if (isRanged == true || isMagic == true)
        {
            switch (ProjectileName)
            {
                case "Arrow":
                    ObjectPooler.Instance.SpawnFromPool("Arrow", attackPoint.transform.position, transform.rotation);
                    break;
                case "GhostLanternAttack":
                    ObjectPooler.Instance.SpawnFromPool("GhostLanternAttack", attackPoint.transform.position, transform.rotation);
                    break;
                default:
                    Debug.Log("Invalid Enemy Projectile Name");
                    break;
            }
        }
    }
}
