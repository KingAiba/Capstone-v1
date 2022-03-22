using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum RoomType
{
    Spawn,
    Small,
    Medium,
    Large,
}

public class Rooms : MonoBehaviour
{
    public List<Doors> doorList = new List<Doors>();
    public List<ValidatorScript> validatorList = new List<ValidatorScript>();

    public List<Transform> spawnPoints;
    public List<Transform> lootPoints;

    public RoomType roomType;

    public MapGenerator mapGenerator;

    public int roomDepth = 0;

    public bool isValid = true;
    public bool isReady = false;
    public bool isSpawnRoom = false;
    public bool isBossRoom = false;

    public NavMeshSurface roomNavMeshSurface;

    public void InitializeRoom()
    {
        //doorList = new List<Doors>(GetComponentsInChildren<Doors>());
        foreach(Doors door in doorList)
        {
            door.currRoom = this;
        }
    }

    public Doors SetAsNormalRoom()
    {
        isSpawnRoom = false;
        isBossRoom = false;

        return doorList[0];
    }

    public void SetAsSpawnPoint()
    {
        isSpawnRoom = true;
        isBossRoom = false;

        roomDepth = 0;

        for(int i = 0; i < doorList.Count; i++)
        {
            if(i == 0)
            {
                doorList[i].SetDoorTriggerEnable(true);
            }
            else
            {
                doorList[i].SetDoorTriggerEnable(false);
            }
        }
    }

    public List<Doors> GetRoomDoors()
    {
        return doorList;
    }

    public bool CheckRoomValidation()
    {
        foreach(ValidatorScript validator in validatorList)
        {
            if(validator.isCollidingWithRoom)
            {
                isValid = false;
                break;
            }
        }

        return isValid;
    }

    public void ActivateValidators()
    {
        foreach (ValidatorScript validator in validatorList)
        {
            validator.gameObject.SetActive(true);
        }
    }

    public void DeactivateValidators()
    {
        foreach(ValidatorScript validator in validatorList)
        {
            validator.gameObject.SetActive(false);
        }
    }

    public void RebuildNavMesh()
    {
        roomNavMeshSurface.BuildNavMesh();
    }

    public void RoomValidationProcedure(MapGenerator mg)
    {
        mapGenerator = mg;
        mapGenerator.OnMapGenerationCompleted += DeactivateValidators;
        mapGenerator.OnMapGenerationCompleted += RebuildNavMesh;
    }

    void Start()
    {
        InitializeRoom();
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        mapGenerator.OnMapGenerationCompleted -= DeactivateValidators;
        mapGenerator.OnMapGenerationCompleted -= RebuildNavMesh;
    }
}
