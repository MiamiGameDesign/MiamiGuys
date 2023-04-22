using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomPlatforms : MonoBehaviour
{

    // the prefab cube to be spawned
    public GameObject cubePrefab;

    // the initial time between cube spawns
    public float initialSpawnTime = 0.5f;

    // the current time between cube spawns
    private float currentSpawnTime;

    private int numberOfCubesSpawned = 0;
    public int maxNumberOfCubes = 2;
    private int maxZ = 6;

    void Update()
    {
        if (Time.realtimeSinceStartup > currentSpawnTime)
        {
            if (numberOfCubesSpawned < maxNumberOfCubes)
            {
                // get a random position from the array of spawn positions
                Vector3 spawnPosition = new Vector3(Random.Range(-5,5), 0, Random.Range(5,maxZ));
                maxZ++;
                // instantiate a new cube at the random position
                Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
                numberOfCubesSpawned++;
            }
            currentSpawnTime = Time.realtimeSinceStartup + (float) 0.15;
        }
    }
}