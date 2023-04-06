using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Tomato;

    [SerializeField] private float offset = 10;
    [SerializeField] private float spawnRate = 2;


    private float timer = 0;


    private void SpawnFlower(){
        float lowestLong = transform.position.x - offset;
        float highestLong = transform.position.x + offset;

        float lowestLat = transform.position.z - offset;
        float highestLat = transform.position.z + offset;

        Instantiate(Tomato, new Vector3(Random.Range(lowestLong, highestLong), transform.position.y, Random.Range(lowestLat, highestLat)), transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate){
            timer +=  Time.deltaTime;
        } else {
            SpawnFlower();
            timer = 0;
        }
    }
}
