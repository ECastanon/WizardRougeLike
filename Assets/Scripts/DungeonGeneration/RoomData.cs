using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public int RoomID;
    public bool hasEntered = false;

    [HideInInspector]
    public GameObject Door, T, B, L, R;
    public GameObject BLRuined, TBLRCenterHole, TLRRuined, TRRuined, TBRuined, LRRuined;

    void Start()
    {
        FixParent();
        AddList();
        SetID();
        CheckIfLast();
        RoomSwap();

    }
    //Sets the room as a parent of "Dungeon Generator"
    void FixParent()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        transform.SetParent(dg.transform);
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
        }
    }
    //Creates Doors upon player's first entry
    //Sets up the related MiniMap Piece
    //Called in GridController
    public void SetDoors()
    {
        if (hasEntered == false)
        {
            if (T != null)
            {
                ObjectPooler.Instance.SpawnFromPool("Door", new Vector2(T.transform.position.x, T.transform.position.y - 1), Quaternion.Euler(0, 0, 0));
            }
            if (B != null)
            {
                ObjectPooler.Instance.SpawnFromPool("Door", new Vector2(B.transform.position.x, B.transform.position.y + 1), Quaternion.Euler(0, 0, 0));
            }
            if (L != null)
            {
                ObjectPooler.Instance.SpawnFromPool("Door", new Vector2(L.transform.position.x + 0.5f, L.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            if (R != null)
            {
                ObjectPooler.Instance.SpawnFromPool("Door", new Vector2(R.transform.position.x - 0.5f, R.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            hasEntered = true;

            GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
            for (int i = 0; i < dg.GetComponent<LevelGeneration>().roomMiniTypes.Count; i++)
            {
                GameObject rmt = dg.GetComponent<LevelGeneration>().roomMiniTypes[i];
                if (rmt.GetComponent<RoomMiniData>().RoomIDMini == RoomID)
                {
                    Debug.Log("4");

                    rmt.GetComponent<RoomMiniData>().EnableMapPiece();
                }
            }
        }
    }
    //Sets up the related MiniMap Piece
    //Called in GridController
    public void PlayerEnter()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        for (int i = 0; i < dg.GetComponent<LevelGeneration>().roomMiniTypes.Count; i++)
        {
            GameObject rmt = dg.GetComponent<LevelGeneration>().roomMiniTypes[i];
            if (rmt.GetComponent<RoomMiniData>().RoomIDMini == RoomID)
            {
                rmt.GetComponent<RoomMiniData>().playerInside = true;
            }
        }
    }
    public void PlayerLeave()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        for (int i = 0; i < dg.GetComponent<LevelGeneration>().roomMiniTypes.Count; i++)
        {
            GameObject rmt = dg.GetComponent<LevelGeneration>().roomMiniTypes[i];
            if (rmt.GetComponent<RoomMiniData>().RoomIDMini == RoomID)
            {
                rmt.GetComponent<RoomMiniData>().playerInside = false;
            }
        }
    }
    //Sets the room's ID
    void SetID()
    {
        GameObject dg = GameObject.FindGameObjectWithTag("Rooms");
        RoomID = dg.GetComponent<LevelGeneration>().setID;
        RoomID = RoomID - dg.GetComponent<LevelGeneration>().numberOfRooms;
        dg.GetComponent<LevelGeneration>().setID++;
    }
    //Changes Room
    void RoomSwap()
    {
        if(this.gameObject.name == "BL-1(Clone)")
        {
            GameObject newRoom = Instantiate(BLRuined, this.gameObject.transform.position, Quaternion.identity);
            newRoom.gameObject.GetComponent<RoomData>().RoomID = this.RoomID;
            this.gameObject.SetActive(false);
        }
        if (this.gameObject.name == "TBLR-1(Clone)")
        {
            GameObject newRoom = Instantiate(TBLRCenterHole, this.gameObject.transform.position, Quaternion.identity);
            newRoom.gameObject.GetComponent<RoomData>().RoomID = this.RoomID;
            this.gameObject.SetActive(false);
        }
        if (this.gameObject.name == "TLR-1(Clone)")
        {
            GameObject newRoom = Instantiate(TLRRuined, this.gameObject.transform.position, Quaternion.identity);
            newRoom.gameObject.GetComponent<RoomData>().RoomID = this.RoomID;
            this.gameObject.SetActive(false);
        }
        if (this.gameObject.name == "TR-1(Clone)")
        {
            GameObject newRoom = Instantiate(TRRuined, this.gameObject.transform.position, Quaternion.identity);
            newRoom.gameObject.GetComponent<RoomData>().RoomID = this.RoomID;
            this.gameObject.SetActive(false);
        }
        if (this.gameObject.name == "TB-1(Clone)")
        {
            GameObject newRoom = Instantiate(TBRuined, this.gameObject.transform.position, Quaternion.identity);
            newRoom.gameObject.GetComponent<RoomData>().RoomID = this.RoomID;
            this.gameObject.SetActive(false);
        }
        if (this.gameObject.name == "LR-1(Clone)")
        {
            GameObject newRoom = Instantiate(LRRuined, this.gameObject.transform.position, Quaternion.identity);
            newRoom.gameObject.GetComponent<RoomData>().RoomID = this.RoomID;
            this.gameObject.SetActive(false);
        }
    }
}
