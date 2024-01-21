using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAttack : MonoBehaviour
{
	public float spd;
	public float rotation;
	public Player player;
	Color oldColor;
	IEnumerator decay;
	float flashspeed = 0f;

	public int timeAlive; //Maxtime until the bullet is destroyed
	public float timer; //Time until the bullet is destroyed
    public int damage = 5;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		timer = timeAlive;
		transform.rotation = Quaternion.Euler (0, 0, rotation);
	}

	// Update is called once per frame
	void Update () {
		ApproachPlayer();
		timer -= Time.deltaTime;
		//Enables Bullet decay
		decay = Decay();
		StartCoroutine(decay);
		if (timer <= 0){Destroy(gameObject);} //Deletes the bullets
	}

	public void ResetTimer(){
		timer = timeAlive;
	}

	public void ApproachPlayer()
	{
		transform.transform.LookAt(player.transform.position);
		transform.Rotate(new Vector3(0, -90, 0), Space.Self);

		if(Vector3.Distance(transform.position, player.transform.position) > .01f)
		{
			transform.Translate(new Vector3(spd* Time.deltaTime, 0, 0));
		}
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player player = col.GetComponent<Player>();
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

	//Bullet slowly disapears
	IEnumerator Decay() 
	{
		while (timer <= 2f)
		{
			oldColor = gameObject.GetComponent<Renderer> ().material.color;
			yield return new WaitForSeconds(.03f - flashspeed);
			gameObject.GetComponent<Renderer> ().material.color = new Color(1f, 1f, 1f, timer/2);
			yield return new WaitForSeconds(.03f -  flashspeed);
			flashspeed += Time.deltaTime;
		}
		flashspeed = 0f;
	}
}
