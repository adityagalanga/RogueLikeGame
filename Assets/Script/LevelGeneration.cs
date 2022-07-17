using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color StartColor;
    public Color EndColor;
    public Color shopColor;
    public Color gunRoomColor;

    public int distanceToEnd;

    public bool includeShop;
    public int minDistanceToShop;
    public int maxDistanceToShop;

    public bool includeGunRoom;
    public int minDistancetoGunRoom;
    public int maxDistancetoGunRoom;

    public Transform generatorPoint;

    public enum  Direction { up,right,down,left};
    public Direction SelectedDirection;

    public float xOffset = 18f;
    public float yOffset = 10f;

    public LayerMask RoomLayer;
    private GameObject endRoom;
    private GameObject shopRoom;
    private GameObject gunRoom;

    private List<GameObject> LayoutRoomObjects = new List<GameObject>();
    private List<GameObject> GeneratedOutline = new List<GameObject>();
    public RoomPrefabs Rooms;

    public RoomCenter centerStart;
    public RoomCenter centerEnd;
    public RoomCenter centerShop;
    public RoomCenter centerGun;

    public RoomCenter[] potencialCenters;

    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = StartColor;
        SelectedDirection = (Direction)Random.Range(0,4);
        MoveGenerationPoint();
        for(int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

            if(i+1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = EndColor;
                endRoom = newRoom;
            }
            else
            {
                LayoutRoomObjects.Add(newRoom);
            }

            SelectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            while(Physics2D.OverlapCircle(generatorPoint.position, .2f, RoomLayer))
            {
                MoveGenerationPoint();
            }
        }

        if (includeShop)
        {
            int shopSelector = Random.Range(minDistanceToShop, maxDistanceToShop+1);
            shopRoom = LayoutRoomObjects[shopSelector];
            LayoutRoomObjects.RemoveAt(shopSelector);
            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;
        }

        if (includeGunRoom)
        {
            int GunSelector = Random.Range(minDistancetoGunRoom, maxDistancetoGunRoom + 1);
            gunRoom = LayoutRoomObjects[GunSelector];
            LayoutRoomObjects.RemoveAt(GunSelector);
            gunRoom.GetComponent<SpriteRenderer>().color = gunRoomColor;
        }

        //create room outline
        CreateRoomOutline(Vector3.zero);
        foreach(GameObject room in LayoutRoomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);

        if (includeShop) 
        { 
            CreateRoomOutline(shopRoom.transform.position); 
        }

        if (includeShop)
        {
            CreateRoomOutline(gunRoom.transform.position);
        }

        foreach (GameObject outline in GeneratedOutline)
        {
            bool generate = true;

            if(outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generate = false;
            }

            if (outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generate = false;
            }

            if (includeShop)
            {
                if (outline.transform.position == shopRoom.transform.position)
                {
                    Instantiate(centerShop, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                    generate = false;
                }
            }

            if (includeShop)
            {
                if (outline.transform.position == gunRoom.transform.position)
                {
                    Instantiate(centerGun, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                    generate = false;
                }
            }

            if (generate)
            {
                int centerSelect = Random.Range(0, potencialCenters.Length);
                Instantiate(potencialCenters[centerSelect], outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
            }
        }
    }

    public void MoveGenerationPoint()
    {
        switch (SelectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;

        }
    }

    public void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, RoomLayer);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), .2f, RoomLayer);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), .2f, RoomLayer);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, RoomLayer);

        int directionCount = 0;
        if (roomAbove) directionCount++;
        if (roomBelow) directionCount++;
        if (roomLeft) directionCount++;
        if (roomRight) directionCount++;

        switch (directionCount)
        {
            case 0:
                break;
            case 1:
                if (roomAbove)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.singleUp, roomPosition, transform.rotation));
                }
                if (roomBelow)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.singleDown, roomPosition, transform.rotation));
                }
                if (roomLeft)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.singleLeft, roomPosition, transform.rotation));
                }
                if (roomRight)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.singleRight, roomPosition, transform.rotation));
                }
                break;
            case 2:
                if (roomAbove && roomBelow)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.doubleUpDown, roomPosition, transform.rotation));
                }
                if (roomLeft && roomRight)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.doubleLeftRight, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.doubleUpRight, roomPosition, transform.rotation));
                }
                if (roomRight && roomBelow)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.doubleRightDown, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.doubleDownLeft, roomPosition, transform.rotation));
                }
                if (roomLeft && roomAbove)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.doubleLeftUp, roomPosition, transform.rotation));
                }
                break;
            case 3:
                if (roomAbove & roomRight & roomBelow)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.tripleUpRightDown, roomPosition, transform.rotation));
                }
                if (roomRight & roomBelow & roomLeft)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.tripleRightDownLeft, roomPosition, transform.rotation));
                }
                if (roomBelow & roomLeft & roomAbove)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.tripleDownLeftUp, roomPosition, transform.rotation));
                }
                if (roomLeft & roomAbove & roomRight)
                {
                    GeneratedOutline.Add(Instantiate(Rooms.tripleLeftUpRight, roomPosition, transform.rotation));
                }
                break;
            case 4:
                GeneratedOutline.Add(Instantiate(Rooms.fourway, roomPosition, transform.rotation));
                break;
        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp;
    public GameObject singleDown;
    public GameObject singleRight;
    public GameObject singleLeft;
    public GameObject doubleUpDown;
    public GameObject doubleLeftRight;
    public GameObject doubleUpRight;
    public GameObject doubleRightDown;
    public GameObject doubleDownLeft;
    public GameObject doubleLeftUp;
    public GameObject tripleUpRightDown;
    public GameObject tripleRightDownLeft;
    public GameObject tripleDownLeftUp;
    public GameObject tripleLeftUpRight;
    public GameObject fourway;
}