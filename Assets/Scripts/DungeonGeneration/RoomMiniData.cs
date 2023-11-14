using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMiniData : MonoBehaviour
{
    public int RoomIDMini;
    public bool playerInside = false;
    public bool hasEnteredMini = false;

    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        FixParent();
        AddList();
        SetID();
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
        if(hasEnteredMini == false)
        {
            sprite.color = Color.clear; //Have not visited
        }
        else if (playerInside == false && hasEnteredMini == true)
        {
            sprite.color = Color.white; //Have visited
        }
        else if(playerInside == true && hasEnteredMini == true)
        {
            sprite.color = Color.yellow; //Inside
        }
    }
}
