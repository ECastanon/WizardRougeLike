using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }

    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();
    public bool hasSpawned = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && hasSpawned == false)
        {
            grid.columns = 16;
            grid.rows = 8;
            GenerateGrid();
            hasSpawned = true;
        }
        if (col.gameObject.tag == "Player")
        {
            //Change the active minimap color to yellow
            GetComponent<RoomData>().PlayerEnter();

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Change the active minimap color back to its original
            GetComponent<RoomData>().PlayerLeave();

        }
    }

    public void GenerateGrid()
    {
        grid.verticalOffset += 4.5f;
        grid.horizontalOffset += 8.5f;
        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.transform.localPosition = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows - grid.verticalOffset));
                go.name = "X: " + x + ", Y: " + y;
                availablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }
        //Activate Entities On Player Entry
        GetComponent<ObjectRoomSpawner>().InitializeObjectSpawning();
        GetComponent<RoomData>().SetDoors();
        //Enable mirrored minimap room
    }
}
