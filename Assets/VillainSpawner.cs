using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] villainPrefabs;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (true)
        {
            yield return wait;

            int rand = Random.Range(0, villainPrefabs.Length);
            GameObject villainToSpawn = villainPrefabs[rand];

            Instantiate(villainToSpawn, transform.position, Quaternion.identity);
        }
    }
}