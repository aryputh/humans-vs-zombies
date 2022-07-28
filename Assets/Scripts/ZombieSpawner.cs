using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject[] spawnpoints;
    public int startAmount;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < startAmount; i++)
		{
            Instantiate(zombiePrefab, spawnpoints[Random.Range(0, spawnpoints.Length + 1)].transform.position, Quaternion.identity);
		}

        StartCoroutine(SpawnZombie());
    }

    IEnumerator SpawnZombie()
	{
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(zombiePrefab, spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position, Quaternion.identity);

        StartCoroutine(SpawnZombie());
    }
}
