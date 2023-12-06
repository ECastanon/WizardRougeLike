using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public List<SpriteRenderer> sr = new List<SpriteRenderer>();
    public List<Color> oldColor = new List<Color>();
    private Enemy enemy;
    private EnemyMovement em;
    private Animator anim;
    private float opacity;
    private bool isActive;
    void Start()
    {
        AddDescendantsWithSprite(transform, sr);
        opacity = 0;

        enemy = GetComponent<Enemy>();
        em = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();

        enemy.enabled = false;
        em.enabled = false;
    }
    private void AddDescendantsWithSprite(Transform parent, List<SpriteRenderer> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                list.Add(child.gameObject.GetComponent<SpriteRenderer>());
                oldColor.Add(child.gameObject.GetComponent<SpriteRenderer>().color);
            }
            AddDescendantsWithSprite(child, list);
        }
    }
    void Update()
    {
        if(opacity <= 1)
        {
            opacity += Time.deltaTime;
            for (int i = 0; i < sr.Count; i++)
            {
                Color alpha = new Color(oldColor[i].r, oldColor[i].g, oldColor[i].b, opacity);
                sr[i].color = alpha;
            }
        } else 
        {
            if(isActive == false)
            {
                enemy.enabled = true;
                em.enabled = true;
                isActive = true;
                enemy.OnActive();
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
