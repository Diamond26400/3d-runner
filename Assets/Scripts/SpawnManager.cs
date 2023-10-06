using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ObstaclePreferb;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    public float SpawnDelay = 2.0f;
    public float SpawnRate = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", SpawnDelay, SpawnRate);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnObstacle()
    {

        Instantiate(ObstaclePreferb, spawnPos, ObstaclePreferb.transform.rotation);
    }
}
