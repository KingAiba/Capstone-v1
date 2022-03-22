using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Rooms currRoom;
    public Rooms nextRoom;
    Doors connectedDoor;

    public MapGenerator _mapGenerator;

    public GameObject connector;
    public List<Transform> connectorConnectionPoints;
    public DoorTrigger doorTrigger;

    public Animator doorAnimator;

    public bool isDoorTriggerEnabled = false;
    public bool isConnected = false;
    //public bool isOpen = false;

    public void SetDoorTriggerEnable(bool isEnabled)
    {
        isDoorTriggerEnabled = isEnabled;
        doorTrigger.gameObject.SetActive(isEnabled);
    }

    public void OpenDoor()
    {
        if(isDoorTriggerEnabled && isConnected)
        {
            doorAnimator.SetBool("character_nearby", !doorAnimator.GetBool("character_nearby"));
        }
    }

    public void InitializeDoor()
    {
        doorAnimator = GetComponent<Animator>();
        doorTrigger = GetComponentInChildren<DoorTrigger>();
        SetDoorTriggerEnable(true);
        doorTrigger.OnDoorTriggerEnter += OpenDoor;
        doorTrigger.OnDoorTriggerExit += OpenDoor;
    }

    public void DestroyDoor()
    {
        doorTrigger.OnDoorTriggerEnter -= OpenDoor;
        doorTrigger.OnDoorTriggerExit -= OpenDoor;
    }

    public void AlignConnectorToDoor()
    {
        Transform c1 = connectorConnectionPoints[0];
        if(transform.rotation.y > 0)
        {
            float rot = Quaternion.Angle(c1.transform.rotation, transform.rotation);
            connector.transform.rotation = Quaternion.AngleAxis(-rot, Vector3.up) * connector.transform.rotation;
        }
        else
        {
            float rot = Quaternion.Angle(c1.transform.rotation, transform.rotation);
            connector.transform.rotation = Quaternion.AngleAxis(rot, Vector3.up) * connector.transform.rotation;
        }

        //Quaternion rot =  c1.rotation * transform.rotation;
        //rot = rot * connector.transform.rotation;
        //connector.transform.rotation = rot;
        connector.transform.position = transform.position - (c1.position - connector.transform.position);
    }

    public void AlignNewRoomToConnector()
    {
        Transform c2 = connectorConnectionPoints[1];
        connectedDoor = nextRoom.SetAsNormalRoom();
        // Rotate room by angel needed to align door to connector point
        Quaternion rot = connectedDoor.transform.rotation * Quaternion.Inverse(c2.rotation);
        rot = rot * nextRoom.transform.rotation;
        nextRoom.transform.rotation = Quaternion.Inverse(rot);
        // set next room position to connector point and then subtract distance between door and room
        nextRoom.transform.position =  c2.position - (connectedDoor.transform.position - nextRoom.transform.position);

        connectedDoor.isConnected = true;
        connectedDoor.SetDoorTriggerEnable(true);
    }

    public Rooms AttachRoom(MapGenerator mapGenerator)
    {
        _mapGenerator = mapGenerator;
        connector = Instantiate(mapGenerator.GenerateRandomConnectorFromCurrentTileSet(), transform.position, Quaternion.identity);

        connectorConnectionPoints = new List<Transform>();

        for(int i =0; i< connector.transform.childCount; i++)
        {
            Transform child = connector.transform.GetChild(i);
            if(child.CompareTag("Connector"))
            {
                connectorConnectionPoints.Add(child);
            }
        }

        AlignConnectorToDoor();
        GameObject roomToSpawn = Instantiate(mapGenerator.GenerateRandomRoomFromCurrentTileSet(), transform.position, Quaternion.identity);
        nextRoom = roomToSpawn.GetComponent<Rooms>();
        nextRoom.roomDepth = currRoom.roomDepth + 1;
        AlignNewRoomToConnector();

        isConnected = true;
        connectedDoor.InitializeConnectedDoor(this, currRoom);

        return nextRoom;
    }

    public void InitializeConnectedDoor(Doors prevDoor, Rooms prevRoom)
    {
        isConnected = true;
        SetDoorTriggerEnable(true);
        connectedDoor = prevDoor;
        nextRoom = prevRoom;
    }

    public void DestroyAttachedRoom()
    {
        Destroy(connector.gameObject);
        Destroy(nextRoom.gameObject);

        nextRoom = null;
        connector = null;

        isConnected = false;
        SetDoorTriggerEnable(false);
    }


    private void Awake()
    {
        InitializeDoor();
    }
    private void Start()
    {
        
    }



    private void OnDestroy()
    {
        DestroyDoor();
    }


}
