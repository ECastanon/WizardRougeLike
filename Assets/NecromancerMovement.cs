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

    private void Update()
    {
        /* enemy and player are Transforms */
        Vector3 axis = new Vector3(0,0,1);
        distance = player.transform.position - transform.position;
        //Debug.Log(distance.magnitude);
        if(distance.magnitude < Radius)
        {
            CirclePlayer();
        } else if(distance.magnitude >= Radius)
        { 
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, travelSpeed * Time.deltaTime);
        }
    }
    private void CirclePlayer()
    {
        transform.RotateAround(player.transform.localPosition, Vector3.back, Time.deltaTime*RotateSpeed);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
