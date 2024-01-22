using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public int damage;
	public float spd;
	public int timeAlive;
	private float timer;

	void Start()
    {
		timer = timeAlive;
	}

	void Update ()
    {
        transform.Translate(Vector2.one * Time.deltaTime * spd);

		timer -= Time.deltaTime;
		if (timer <= 0){Destroy(gameObject);}
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
}
