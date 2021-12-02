using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    public GameObject[] hazards;
    float timer=0;
    float spawnTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(LoadFluf.start)
        timer += Time.deltaTime;
        else
        {
            timer = 0;
            spawnTime = 1.5f;
        }

        if (timer > spawnTime)
        {
            Instantiate(hazards[Random.Range(0, hazards.Length)]);
            spawnTime -= Time.deltaTime*5;
            if (spawnTime < .75f)
                spawnTime = .75f;
            timer = Random.Range(0, spawnTime / 5);
        }
    }
}
