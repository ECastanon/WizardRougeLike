using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public int RoomID;
    public bool idOverride = false;
    public bool hasEntered = false;
    public string roomTyp;
    private GameObject gameManager;
    private GameObject player;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        AddList();
        CheckIfLast();
    }

    //Sets all rooms as a parent of "Dungeon Generator"
    void FixAllParent()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        foreach(var room in dg.GetComponent<LevelGeneration>().roomTypes)
        {
            room.transform.SetParent(dg.transform);
        }
    }
    //Adds the created room to a list of other rooms
    void AddList()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        dg.GetComponent<LevelGeneration>().roomTypes.Add(this.gameObject);
        dg.GetComponent<LevelGeneration>().roomCount++;
    }
    //Checks if the room was the last one to be created
    void CheckIfLast()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        if (dg.GetComponent<LevelGeneration>().roomCount >= dg.GetComponent<LevelGeneration>().numberOfRooms)
        {
            dg.GetComponent<LevelGeneration>().SetLadders();
            FixAllParent();
        }
    }
    //Creates Doors upon player's first entry
    //Sets up the related MiniMap Piece
    //Called in GridController
    public void SetDoors()
    {
        string doorString = gameObject.name;
        if (hasEntered == false)
        {
            if (doorString.Contains("T"))
            {
                ObjectPooler.Instance.SpawnFromPool("Door", new Vector2(transform.position.x, transform.position.y + 4.5f), Quaternion.Euler(0, 0, 0));
            }
            if (doorString.Contains("B"))
            {
                ObjectPooler.Instance.SpawnFromPool("Door", new Vector2(transform.position.x, transform.position.y - 4.5f), Quaternion.Euler(0, 0, 0));
            }
            if (doorString.Contains("L"))
            {
                ObjectPooler.Instance.SpawnFromPool("Door", new Vector2(transform.position.x - 9, transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            if (doorString.Contains("R"))
            {
                ObjectPooler.Instance.SpawnFromPool("Door", new Vector2(transform.position.x + 9, transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            hasEntered = true;
        }
    }
    //Sets up the related MiniMap Piece
    //Called in GridController
    public void PlayerEnter()
    {
        LevelGeneration dg = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelGeneration>();
        for (int i = 0; i < dg.roomMiniTypes.Count; i++)
        {
            RoomMiniData rmt = dg.roomMiniTypes[i].GetComponent<RoomMiniData>();
            if (rmt.RoomIDMini == RoomID)
            {
                rmt.hasEnteredMini = true;
                rmt.playerInside = true;
            }
        }

        //Passes current Room Type to Relic Panel
        GameObject relicMenu = GameObject.FindGameObjectWithTag("RelicMenu");
        GetRoomType();
        relicMenu.GetComponent<RelicPanel>().roomType = roomTyp;

        //ONLY USED IF THE HOLY EMBRACE IS ACTIVE
        if (gameManager.GetComponent<RelicEffects>().hEmbrace > 0)
        {
            player.GetComponent<Player>().holyEmbrace = true;
        }

        //==Remove after Fixing===
        //Destroy(this.gameObject);
        //========================
    }
    public void PlayerLeave()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        for (int i = 0; i < dg.GetComponent<LevelGeneration>().roomMiniTypes.Count; i++)
        {
            GameObject rmt = dg.GetComponent<LevelGeneration>().roomMiniTypes[i];
            if (rmt != null && rmt.GetComponent<RoomMiniData>().RoomIDMini == RoomID)
            {
                rmt.GetComponent<RoomMiniData>().playerInside = false;
            }
        }
    }
    //Gets the room type
    void GetRoomType()
    {
        Transform roomCenter = this.transform.Find("RoomCenter");
        roomTyp = roomCenter.tag.ToString();
        //Debug.Log("RoomType: " + roomTyp);
    }
}