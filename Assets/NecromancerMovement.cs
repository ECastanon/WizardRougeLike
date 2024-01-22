using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerMovement : MonoBehaviour
{
    public float RotateSpeed = 1f;
    public float travelSpeed = 3f;
    public float Radius = 5f;
    public GameObject player;
    private float angle;
    public Vector2 distance;
    private float posX, posY;
    private SpriteRenderer sp;
    private bool flip = false;
    
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        /* enemy and player are Transforms */
        Vector3 axis = new Vector3(0,0,1);
        distance = player.transform.position - transform.position;
        //Debug.Log(distance.magnitude);
        if(distance.magnitude < Radius)
        {
            if(GetComponent<NecromancerAttacks>().isPhaseTwo)
            {
               int rand = Random.Range(0, 2);
               if(rand == 1){RotateSpeed *= -1;}
            }
            CirclePlayer();
        } else if(distance.magnitude >= Radius)
        { 
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, travelSpeed * Time.deltaTime);
        }
        FacePlayer();
    }
    private void CirclePlayer()
    {
        transform.RotateAround(player.transform.localPosition, Vector3.back, Time.deltaTime*RotateSpeed);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void FacePlayer()
    {
        if (player.transform.position.x < transform.position.x && flip == false)
        {
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            flip = true;
        }
        if (player.transform.position.x >= transform.position.x && flip == true)
        {
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            flip = false;
        }
    }
}
