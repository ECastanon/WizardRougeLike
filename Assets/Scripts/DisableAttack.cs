using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAttack : MonoBehaviour
{
    private EnemyAttacks ea;

    void Start()
    {
        ea = transform.parent.gameObject.GetComponent<EnemyAttacks>();
    }
    public void TurnOffAttack()
    {
        ea.disableAim();
    }
    public void RangedAttack()
    {
        ea.AttackType1();
    }
        public void MagicAttack()
    {
        ea.AttackType1();
    }
}
