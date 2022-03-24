using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileSet
{
    public List<GameObject> RoomPrefabs;
    public List<GameObject> ConnectorPrefabs;
}

public class MapGenerator : MonoBehaviour
{
    public List<TileSet> TileSets = new List<TileSet>();

    public int selectedTileSet = 0;
    public int maxDepthCount = 2;

    public List<Rooms> currentMapRooms = new List<Rooms>();

    private Queue<Rooms> roomQueue = new Queue<Rooms>();

    public bool isMapGenerating = false;
    public bool isMapReady = false;

    public bool canGenRooms = true;
    public float genCooldown = 2f;

    public delegate void OnMapGenerationCompletedDelegate();
    public OnMapGenerationCompletedDelegate OnMapGenerationCompleted;

    public void GenerateMapStart()
    {
        if(currentMapRooms.Count <= 0)
        {
            Rooms generatedRoom = GenerateSpawnRoom();
            generatedRoom.RoomValidationProcedure(this);
            currentMapRooms.Add(generatedRoom);
            roomQueue.Enqueue(generatedRoom);
        }
    }

    IEnumerator MapGeneratorCorutine()
    {
        isMapGenerating = true;
        while (roomQueue.Count > 0)
        {
            
            Rooms currRoom = roomQueue.Dequeue();
            List<Doors> curRoomDoors = currRoom.GetRoomDoors();
            if (currRoom.roomDepth < maxDepthCount)
            {
                foreach (Doors door in curRoomDoors)
                {
                    if (door.isDoorTriggerEnabled && !door.isConnected)
                    {
                        door.currRoom = currRoom;
                        Rooms newRoom = door.AttachRoom(this);

                        yield return new WaitForFixedUpdate();

                        bool validCheck = newRoom.CheckRoomValidation();
                        newRoom.RoomValidationProcedure(this);

                        if (!validCheck)
                        {
                            door.DestroyAttachedRoom();
                        }
                        else
                        {
                            
                            roomQueue.Enqueue(newRoom);
                            currentMapRooms.Add(newRoom);
                        }

                    }
                }
            }
        }
        isMapGenerating = false;
        isMapReady = true;
    }

    public void MapGenerationProcedure()
    {
        if (!isMapReady && !isMapGenerating)
        {
            StartCoroutine(MapGeneratorCorutine());
        }


    }

    public void MapCompleted()
    {
        if (isMapReady && !isMapGenerating)
        {
            OnMapGenerationCompleted?.Invoke();
        }      
    }

    public Rooms GenerateSpawnRoom()
    {
        Rooms generatedRoom = Instantiate(TileSets[selectedTileSet].RoomPrefabs
            .Find(room => room.GetComponent<Rooms>().roomType == RoomType.Spawn))
            .GetComponent<Rooms>();
        generatedRoom.mapGenerator = this;
        generatedRoom.InitializeRoom(true);
        generatedRoom.SetAsSpawnPoint();

        return generatedRoom;
    }
    public GameObject GenerateRandomRoomFromCurrentTileSet()
    {
        return TileSets[selectedTileSet].RoomPrefabs[Random.Range(0, TileSets[selectedTileSet].RoomPrefabs.Count)];
    }

    public GameObject GenerateRandomConnectorFromCurrentTileSet()
    {
        return TileSets[selectedTileSet].ConnectorPrefabs[Random.Range(0, TileSets[selectedTileSet].ConnectorPrefabs.Count)];
    }



    void Start()
    {
        GenerateMapStart();
    }

    void Update()
    {
        MapGenerationProcedure();
        MapCompleted();
    }
}
