using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroField : MonoBehaviour
{
    public int damage;
    public float damageInterval;
    public float timeAlive;
    private float timer;
    private float lifeTime;

    [Header("Debuff Info")]
    [Tooltip("Enter: 'add' OR 'multiply'")] public string operation;
    [Tooltip("How long the speed change effect is in seconds")] public float effectTime;
    [Tooltip("Changes player speed by this amount.")] public float speedInfluence;
    private bool hasReduced = false;
    public void OnObjectSpawn()
    {
        timer = damageInterval;
        lifeTime = 0;
    }
    void Update()
    {
        if(timer < damageInterval)
        {
            timer += Time.deltaTime;
        }
        if(lifeTime < timeAlive)
        {
            lifeTime += Time.deltaTime;
        } else{StartCoroutine(scaleOverTime(transform, new Vector3(0, 0, 1), 3f));}
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Damage within intervals
            if(timer >= damageInterval)
            {
                col.GetComponent<Player>().TakeDamage(damage);
                timer = 0;
            }

            //Reduces player speed
            //Cannot stack
            if(hasReduced == false)
            {
                StartCoroutine(col.GetComponent<PlayerMovement>().AdjustSpeed(operation, speedInfluence, effectTime, true));
                hasReduced = true;
                StartCoroutine(ReduceBool());
            }
        }
    }
    private IEnumerator ReduceBool()
    {
        yield return new WaitForSeconds(effectTime);
        hasReduced = false;
    }
    IEnumerator scaleOverTime(Transform objectToScale, Vector3 toScale, float duration)
    {
        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = objectToScale.localScale;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            yield return null;
        }
        timer = damageInterval;
        lifeTime = 0;
        gameObject.transform.localScale += new Vector3(2, 2, 1);
        gameObject.SetActive(false); //Disables objects to be reused
    }
}
