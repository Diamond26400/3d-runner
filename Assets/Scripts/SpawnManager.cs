using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] ObstaclePreferbs;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    public float SpawnDelay = 2.0f;
    public float SpawnRate = 2.0f;
    private PlayerController PlayerControllerScript;
    private float leftBound = -15;
    private int spawnRandom;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", SpawnDelay, SpawnRate);
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnObstacle()
    {
        //Freezes the Game
        if (PlayerControllerScript.GameOver == false)
        {
            spawnRandom = Random.Range(0, ObstaclePreferbs.Length);
            Instantiate(ObstaclePreferbs[spawnRandom], spawnPos, ObstaclePreferbs[spawnRandom].transform.rotation);
            if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
        // destroy Obstacle
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
