using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    public Vector2 highScreenBounds, lowScreenBounds;
    private int direction;
    public bool isFreeCam = false;

    void Start()
    {
        FindBounds();
    }

    void Update()
    {
        if(player != null && !isFreeCam)
        {
            if(player.transform.position.y > highScreenBounds.y) //Move up
            {
                //Debug.Log("MoveCamera");
                ScrollCamera(1);
            }
            if (player.transform.position.y < lowScreenBounds.y) //Move down
            {
                //Debug.Log("MoveCamera");
                ScrollCamera(2);
            }
            if (player.transform.position.x > highScreenBounds.x) //Move left
            {
                //Debug.Log("MoveCamera");
                ScrollCamera(3);
            }
            if (player.transform.position.x < lowScreenBounds.x) //Move right
            {
                //Debug.Log("MoveCamera");
                ScrollCamera(4);
            }
        }
        if(isFreeCam)
        {
            transform.position = player.transform.position + new Vector3(0, 0, -100);
        }
    }
    void FindBounds()
    {
        highScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        lowScreenBounds = highScreenBounds - new Vector2(20,11);

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
