using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;
    public GameObject player;
    public GameObject exit;
    private int direction;
    public float moveAmount;
    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;
    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration;
    public LayerMask room;
    private int downCounter;
    public int chack = 0;
    public int randStartingPos;
    public int randExitPos;

    private void Start()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Rooms"));
        chack = chack + 1;
        randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[1], transform.position, Quaternion.identity, GameObject.Find("Rooms(Clone)").transform);
        if (chack == 1) //�������� ������ � ������ �������
        {
            transform.position = startingPositions[randStartingPos].position;
            player.transform.position = transform.position;
        }

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (stopGeneration == false)
        {
            Move();
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
                int rand = Random.Range(0, rooms.Length);
                randExitPos = rand;
                Instantiate(rooms[rand], transform.position, Quaternion.identity, GameObject.Find("Rooms(Clone)").transform);
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                int rand = Random.Range(0, rooms.Length);
                randExitPos = rand;
                Instantiate(rooms[rand], transform.position, Quaternion.identity, GameObject.Find("Rooms(Clone)").transform);
                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5)
        {
            downCounter++;
            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity, GameObject.Find("Rooms(Clone)").transform);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity, GameObject.Find("Rooms(Clone)").transform);
                    }
                }
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;
                int rand = Random.Range(2, 4);
                randExitPos = rand;
                Instantiate(rooms[rand], transform.position, Quaternion.identity, GameObject.Find("Rooms(Clone)").transform);
                direction = Random.Range(1, 6);

            }
            else
            {
                stopGeneration = true;
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y);
                transform.position = newPos;
                exit.transform.position = transform.position;
            }
        }
    }
}
