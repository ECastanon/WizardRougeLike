using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IPooledObject
{
    public float speed;
    public int damage;
    public int timeAlive;
    public float timer;

    public Rigidbody2D rb;
    Vector3 playerLocation;
    private GameObject player;

    public void OnObjectSpawn()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Get Player location and faces towards the player
        playerLocation = player.transform.position;
        ResetTimer();
        FacePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
        Timer();
    }
    public void FacePlayer()
    {
        Vector2 direction = playerLocation - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
    public void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= timeAlive) gameObject.SetActive(false); //Disables objects to be reused
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Attack has collided with the Player");
            Player player = col.GetComponent<Player>();
            player.TakeDamage(damage);
            gameObject.SetActive(false); //Disables objects to be reused
        }
    }
    public void ResetTimer()
    {
        timer = 0;
    }
}
