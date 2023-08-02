using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMiniData : MonoBehaviour
{
    public int RoomIDMini;
    public bool playerInside = false;

    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        FixParent();
        AddList();
        SetID();
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
    }
    void Update()
    {
        Highlight();
    }

    void FixParent()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("MiniMap");
        transform.SetParent(dg.transform);
        transform.position += new Vector3(-6, 3, 0);
    }
    public void EnableMapPiece()
    {
        //Debug.Log("EnableMapPiece");
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    void AddList()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        dg.GetComponent<LevelGeneration>().roomMiniTypes.Add(this.gameObject);
        dg.GetComponent<LevelGeneration>().roomMiniCount++;
    }
    void SetID()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        RoomIDMini = dg.GetComponent<LevelGeneration>().setID;
        dg.GetComponent<LevelGeneration>().setID++;
    }
    void Highlight()
    {
        if(playerInside == true)
        {
            sprite.color = new Color(1, 1, 0, 1);
        }
        if (playerInside == false)
        {
            sprite.color = new Color(1, 1, 1, 1);
        }
    }
}
