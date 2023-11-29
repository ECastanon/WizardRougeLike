using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    private SpriteRenderer sr;
    private Enemy enemy;
    private EnemyMovement em;
    private Animator anim;
    private float opacity;
    private bool isActive;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        enemy = GetComponent<Enemy>();
        em = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();

        sr.color = new Color(1,1,1,0);
        enemy.enabled = false;
        em.enabled = false;
    }
    void Update()
    {
        if(opacity <= 1)
        {
            opacity += Time.deltaTime;
            Color alpha = new Color(1,1,1,opacity);
            sr.color = alpha;
        } else 
        {
            if(isActive == false)
            {
                enemy.enabled = true;
                em.enabled = true;
                isActive = true;
            }
        }
    }
    public void Deactivate()
    {
        enemy.enabled = false;
        em.enabled = false;
        anim.enabled = false;
    }
}
