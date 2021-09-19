using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] RoomPrefabs;
    public int numOfRooms;
    public char[] RoomPrefabExitTypes;

    public GameObject startRoom;
    public GameObject finishRoom;
    public GameObject playerPrefab;

    public GameObject[] enemyPrefabs;

    private GameObject currentRoom;
    private bool inRoom;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
        inRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inRoom)
        {
            if(currentRoom.GetComponentInChildren<EnemyMovement>() == null)
            {
                inRoom = false;
                currentRoom.transform.Find("Doors/Exit").gameObject.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void GenerateLevel()
    {
        Vector3 spawnLoc;
        char previousRoomType = 'B';

        GameObject start = Instantiate(startRoom);
        start.transform.position = Vector3.zero;
        spawnLoc = start.transform.Find("Exit").transform.position;

        currentRoom = start;

        GameObject player = Instantiate(playerPrefab);
        player.transform.position = start.transform.Find("PlayerStart").transform.position;

        for (int i = 0; i < numOfRooms; i++)
        {
            int roomIndex = Random.Range(0, RoomPrefabs.Length);
            GameObject room = Instantiate(RoomPrefabs[roomIndex]);

            room.transform.Find("Doors/" + previousRoomType).gameObject.SetActive(false);

            Vector3 positionOffset = room.transform.position - room.transform.Find("Entrances/" + previousRoomType).transform.position;
            
            room.transform.position = spawnLoc + positionOffset;

            spawnLoc = room.transform.Find("Exit").transform.position;
            previousRoomType = RoomPrefabExitTypes[roomIndex];
        }

        GameObject finish = Instantiate(finishRoom);

        Vector3 positionOffset2 = finish.transform.position - finish.transform.Find("Entrances/" + previousRoomType).transform.position;

        finish.transform.position = spawnLoc + positionOffset2;
    }

    public void EnterRoom(GameObject room)
    {
        room.transform.Find("Doors/A").gameObject.SetActive(true);
        room.transform.Find("Doors/B").gameObject.SetActive(true);

        currentRoom = room;
        inRoom = true;

        foreach (Transform spawner in room.transform.Find("Enemies"))
        {
            int num = Random.Range(0, enemyPrefabs.Length);

            GameObject enemy = Instantiate(enemyPrefabs[num], room.transform);
            enemy.transform.position = spawner.transform.position;
        }
        
    }
}
