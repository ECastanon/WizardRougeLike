using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    public Vector2 highScreenBounds;
    public Vector2 lowScreenBounds;

    private int direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FindBounds();
    }

    void LateUpdate()
    {
        if(player.transform.position.y > highScreenBounds.y) //Move up
        {
            Debug.Log("MoveCamera");
            ScrollCamera(1);
        }
        if (player.transform.position.y < lowScreenBounds.y) //Move down
        {
            Debug.Log("MoveCamera");
            ScrollCamera(2);
        }
        if (player.transform.position.x > highScreenBounds.x) //Move left
        {
            Debug.Log("MoveCamera");
            ScrollCamera(3);
        }
        if (player.transform.position.x < lowScreenBounds.x) //Move right
        {
            Debug.Log("MoveCamera");
            ScrollCamera(4);
        }
    }
    void FindBounds()
    {
        highScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        lowScreenBounds = highScreenBounds - new Vector2(20, 10);

    }
    void ScrollCamera(int direction)
    {
        if(direction == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 12, transform.position.z);
            player.transform.position += new Vector3(0, 3);
        }
        if (direction == 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 12, transform.position.z);
            player.transform.position += new Vector3(0, -3);
        }
        if (direction == 3)
        {
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            player.transform.position += new Vector3(3, 0);
        }
        if (direction == 4)
        {
            transform.position = new Vector3(transform.position.x - 20, transform.position.y, transform.position.z);
            player.transform.position += new Vector3(-3, 0);
        }

        //rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

        FindBounds();
        direction = 0;
    }
}
