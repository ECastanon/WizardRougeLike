using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    private Vector2 worldSize = new Vector2(4, 4);
    Room[,] rooms;
    List<Vector2> takenPositions = new List<Vector2>();
    private int gridSizeX, gridSizeY;
    public GameObject dungeonGenerator;
    public bool isBossRoom = false;

    [Header("Room Generation Data")]
    public int numberOfRooms;
    public int roomCount;
    public List<GameObject> roomTypes = new List<GameObject>();
    public int roomMiniCount;
    public List<GameObject> roomMiniTypes = new List<GameObject>();
    public bool ladderSpawned = false;
    public bool SpawnRoomExists = false;
    public bool isFirstFloor = true; //When false, relocates the player to the spawn roon rather than creating a new player

    [Header("Prefabs")]
    public GameObject player;
    public GameObject roomWhiteObj;
    public GameObject roomTemplate;
    public GameObject ladderRoomT;
    public GameObject ladderRoomB;
    public GameObject ladderRoomL;
    public GameObject ladderRoomR;
    public GameObject spawnRoomT;
    public GameObject spawnRoomB;
    public GameObject spawnRoomL;
    public GameObject spawnRoomR;

    [Header("Room IDs")]
    public int setID;
    public int setMiniID;

    void Start()
    {
        if(!isBossRoom){StartDungeonGeneration();}
        else {
            GameObject playerToSpawn;
            CameraFollow cam = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
            playerToSpawn = Instantiate(player, transform.position, Quaternion.identity);
            cam.player = playerToSpawn;
            cam.transform.position = new Vector3(playerToSpawn.transform.position.x, playerToSpawn.transform.position.y, -100);
            GameObject.FindGameObjectWithTag("Boss").GetComponent<NecromancerMovement>().player = playerToSpawn;
        }
    }
    public void StartDungeonGeneration()
    {
        if(numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);

        CreateRooms();
        SetRoomDoors();
        DrawMap();
        DrawMiniMap();

        foreach (Transform child in transform)
        {
            Debug.Log(child);
        }
    }
    public void ClearDungeon()
    {
        foreach(Transform room in transform) //Destroys all rooms
        {
            Destroy(room.gameObject);
        }
        foreach(Transform mini in GameObject.Find("MiniMap").transform) //Destroys all minimaps
        {
            Destroy(mini.gameObject);
        }
        //Clears dungeon data
        roomCount = 0;
        roomTypes.Clear();
        roomMiniCount = 0;
        roomMiniTypes.Clear();
        ladderSpawned = false;
        SpawnRoomExists = false;
        setID = 0;
        setMiniID = 0;
    }

    void CreateRooms()
    {
        //Setup
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
            //Places first room at the center of the grid
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;
        //
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        //Add Rooms
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            float randomPerc = ((float)i) / ((float)numberOfRooms - 1);
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            //Grap New Position
            checkPos = NewPosition();
            //Test New Position
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if(iterations >= 50)
                {
                    print("Error: Could not create with fewer neighbors than: " + NumberOfNeighbors(checkPos, takenPositions));
                }
            }
            //Finalize Position
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);
            takenPositions.Insert(0, checkPos);
        }
    }
    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        return checkingPos;
    }
    Vector2 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if(inc >= 100)
        {
            print("Error: Could not find position with only one neighbor.");
        }
        return checkingPos;
    }
    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;
        if (usedPositions.Contains(checkingPos + Vector2.right))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }
    void SetRoomDoors()
    {
        for (int x = 0; x < (gridSizeX * 2); x++)
        {
            for (int y = 0; y < (gridSizeY * 2); y++)
            {
                if(rooms[x,y] == null)
                {
                    continue;
                }
                Vector2 gridPosition = new Vector2(x, y);
                if(y - 1 < 0) //Check Up
                {
                    rooms[x, y].doorBot = false;
                }
                else
                {
                    rooms[x, y].doorBot = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2) //Check Down
                {
                    rooms[x, y].doorTop = false;
                }
                else
                {
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null);
                }
                if (x - 1 < 0) //Check Left
                {
                    rooms[x, y].doorLeft = false;
                }
                else
                {
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2) //Check Right
                {
                    rooms[x, y].doorRight = false;
                }
                else
                {
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);
                }
            }
        }
    }
    void DrawMap()
    {
        foreach (Room room in rooms)
        {
            if(room == null)
            {
                continue;
            }
            Vector2 drawPos = room.gridPos;
            drawPos.x *= 20;
            drawPos.y *= 12;
            MapPrefabSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapPrefabSelector>();
            mapper.idToPass = setID;
            setID++;
            mapper.up = room.doorTop;
            mapper.down = room.doorBot;
            mapper.left = room.doorLeft;
            mapper.right = room.doorRight;
        }
    }
    void DrawMiniMap()
    {
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue;
            }
            Vector2 drawPos = room.gridPos;
            drawPos.x *= .55f; //.55f
            drawPos.y *= .4f; //.4f
            drawPos.x += 13.5f;
            drawPos.y += 1;
            
            MapPrefabSelector miniMapper = Object.Instantiate(roomTemplate, drawPos, Quaternion.identity).GetComponent<MapPrefabSelector>();
            miniMapper.isMini = true;
            miniMapper.idToPass = setMiniID;
            setMiniID++;
            miniMapper.up = room.doorTop;
            miniMapper.down = room.doorBot;
            miniMapper.left = room.doorLeft;
            miniMapper.right = room.doorRight;
        }
    }
    //Called by RoomData
    //Also sets the spawn room
    public void SetLadders()
    {
        List<GameObject> PotentialLadderRooms = new List<GameObject>();
        //Debug.Log("Attempting to create ladder");
        //Debug.Log("Items in room list: " + roomTypes.Count);

        for (int i = 0; i < roomTypes.Count; i++)
        {
            if (roomTypes[i].gameObject.tag == "EndRoom" || roomTypes[i].gameObject.tag == "EndRoomB" || roomTypes[i].gameObject.tag == "EndRoomL" || roomTypes[i].gameObject.tag == "EndRoomR")
            {
                PotentialLadderRooms.Add(roomTypes[i]);
            }
        }

        int randomRoom = Random.Range(0, PotentialLadderRooms.Count);
        if (PotentialLadderRooms[randomRoom].gameObject.tag == "EndRoom")
        {
            if (ladderSpawned == false)
            {
                GameObject ladder = Instantiate(ladderRoomT, PotentialLadderRooms[randomRoom].transform.position, Quaternion.identity);
                ladder.GetComponent<RoomData>().idOverride = true;
                ladder.GetComponent<RoomData>().RoomID = PotentialLadderRooms[randomRoom].GetComponent<RoomData>().RoomID;
                PotentialLadderRooms[randomRoom].gameObject.SetActive(false);
                ladderSpawned = true;
                roomTypes.Add(ladder);
            }
        }
        if (PotentialLadderRooms[randomRoom].gameObject.tag == "EndRoomB")
        {
            if (ladderSpawned == false)
            {
                GameObject ladder = Instantiate(ladderRoomB, PotentialLadderRooms[randomRoom].transform.position, Quaternion.identity);
                ladder.GetComponent<RoomData>().idOverride = true;
                ladder.GetComponent<RoomData>().RoomID = PotentialLadderRooms[randomRoom].GetComponent<RoomData>().RoomID;
                PotentialLadderRooms[randomRoom].gameObject.SetActive(false);
                ladderSpawned = true;
                roomTypes.Add(ladder);
            }
        }
        if (PotentialLadderRooms[randomRoom].gameObject.tag == "EndRoomL")
        {
            if (ladderSpawned == false)
            {
                GameObject ladder = Instantiate(ladderRoomL, PotentialLadderRooms[randomRoom].transform.position, Quaternion.identity);
                ladder.GetComponent<RoomData>().idOverride = true;
                ladder.GetComponent<RoomData>().RoomID = PotentialLadderRooms[randomRoom].GetComponent<RoomData>().RoomID;
                PotentialLadderRooms[randomRoom].gameObject.SetActive(false);
                ladderSpawned = true;
                roomTypes.Add(ladder);
            }
        }
        if (PotentialLadderRooms[randomRoom].gameObject.tag == "EndRoomR")
        {
            if (ladderSpawned == false)
            {
                GameObject ladder = Instantiate(ladderRoomR, PotentialLadderRooms[randomRoom].transform.position, Quaternion.identity);
                ladder.GetComponent<RoomData>().idOverride = true;
                ladder.GetComponent<RoomData>().RoomID = PotentialLadderRooms[randomRoom].GetComponent<RoomData>().RoomID;
                PotentialLadderRooms[randomRoom].gameObject.SetActive(false);
                ladderSpawned = true;
                roomTypes.Add(ladder);
            }
        }
        GetSpawnRoom(randomRoom, PotentialLadderRooms);
    }
    private void GetSpawnRoom(int randomRoom, List<GameObject> PotentialLadderRooms)
    {
        int spawnedRoom = Random.Range(0, PotentialLadderRooms.Count);
        GameObject roomToSpawn;
        while(spawnedRoom == randomRoom)
        {
            spawnedRoom = Random.Range(0, PotentialLadderRooms.Count);
        }
        if(SpawnRoomExists == false)
        {
            if (PotentialLadderRooms[spawnedRoom].gameObject.tag == "EndRoom")
            {
                roomToSpawn = Instantiate(spawnRoomT, PotentialLadderRooms[spawnedRoom].transform.position, Quaternion.identity);
                SetPlayer(spawnedRoom, PotentialLadderRooms);
                roomToSpawn.GetComponent<RoomData>().idOverride = true;
                roomToSpawn.GetComponent<RoomData>().RoomID = PotentialLadderRooms[spawnedRoom].GetComponent<RoomData>().RoomID;
                PotentialLadderRooms[spawnedRoom].gameObject.SetActive(false);
                SpawnRoomExists = true;

            }
            if (PotentialLadderRooms[spawnedRoom].gameObject.tag == "EndRoomB")
            {
                roomToSpawn = Instantiate(spawnRoomB, PotentialLadderRooms[spawnedRoom].transform.position, Quaternion.identity);
                SetPlayer(spawnedRoom, PotentialLadderRooms);
                roomToSpawn.GetComponent<RoomData>().idOverride = true;
                roomToSpawn.GetComponent<RoomData>().RoomID = PotentialLadderRooms[spawnedRoom].GetComponent<RoomData>().RoomID;
                PotentialLadderRooms[spawnedRoom].gameObject.SetActive(false);
                SpawnRoomExists = true;
            }
            if (PotentialLadderRooms[spawnedRoom].gameObject.tag == "EndRoomL")
            {
                roomToSpawn = Instantiate(spawnRoomL, PotentialLadderRooms[spawnedRoom].transform.position, Quaternion.identity);
                SetPlayer(spawnedRoom, PotentialLadderRooms);
                roomToSpawn.GetComponent<RoomData>().idOverride = true;
                roomToSpawn.GetComponent<RoomData>().RoomID = PotentialLadderRooms[spawnedRoom].GetComponent<RoomData>().RoomID;
                PotentialLadderRooms[spawnedRoom].gameObject.SetActive(false);
                SpawnRoomExists = true;
            }
            if (PotentialLadderRooms[spawnedRoom].gameObject.tag == "EndRoomR")
            {
                roomToSpawn = Instantiate(spawnRoomR, PotentialLadderRooms[spawnedRoom].transform.position, Quaternion.identity);
                SetPlayer(spawnedRoom, PotentialLadderRooms);
                roomToSpawn.GetComponent<RoomData>().idOverride = true;
                roomToSpawn.GetComponent<RoomData>().RoomID = PotentialLadderRooms[spawnedRoom].GetComponent<RoomData>().RoomID;
                PotentialLadderRooms[spawnedRoom].gameObject.SetActive(false);
                SpawnRoomExists = true;
            }  
        }
    }
    private void SetPlayer(int spawnedRoom, List<GameObject> PotentialLadderRooms)
    {
        GameObject playerToSpawn;
        CameraFollow cam = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        if(isFirstFloor == true)
        {
            playerToSpawn = Instantiate(player, PotentialLadderRooms[spawnedRoom].transform.position, Quaternion.identity);
            cam.player = playerToSpawn;
            cam.transform.position = new Vector3(playerToSpawn.transform.position.x, playerToSpawn.transform.position.y, -100);
            isFirstFloor = false;
        } else {
            playerToSpawn = GameObject.FindGameObjectWithTag("Player");
            playerToSpawn.transform.position = PotentialLadderRooms[spawnedRoom].transform.position;
        }

    }
}