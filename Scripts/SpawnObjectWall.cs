using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectWall : MonoBehaviour
{
    public GameObject[] objects;
    private void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
