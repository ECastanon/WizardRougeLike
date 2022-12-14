using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed; //Base speed of the enemy
    private float curspeed; //Current speed of the enemy: Can be slowed or sped up
    public float range;
    public bool inRange = false;
    public bool isAiming = false;
    private float distanceToPlayer;

    Vector3 movement;
    private bool flip = false;

    private GameObject player;

    [HideInInspector]
    public Animator animator;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        curspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Player location and faces towards the player
        movement = player.transform.position;
        distanceToPlayer = Vector2.Distance(transform.position, movement);

        FacePlayer();
    }
    void FixedUpdate()
    {
        ApproachPlayer();
    }

    public void ApproachPlayer()
    {
        animator.SetBool("isWalking", true);
        if (distanceToPlayer > range && isAiming == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, movement, curspeed * Time.deltaTime);
            inRange = false;
            animator.SetBool("isAiming", false);
        }
        if (distanceToPlayer < range)
        {
            animator.SetBool("isWalking", false);
            inRange = true;
        }
    }
    public void FacePlayer()
    {
        if (movement.x < transform.position.x && flip == false)
        {
            transform.localScale = new Vector2(-1, 1);
            flip = true;
        }
        if (movement.x >= transform.position.x && flip == true)
        {
            transform.localScale = new Vector2(1, 1);
            flip = false;
        }
    }
}