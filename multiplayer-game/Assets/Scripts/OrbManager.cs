using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public GameObject orbPrefab;
    public float spawnInterval = 1f;

    [Header("Orb Spawn Point")]
    public Vector2 horizontalSpawnRange = new Vector2(-6.5f, 6.5f);
    public Vector2 verticalSpawnRange = new Vector2(5.5f, -3.5f);

    private List<GameObject> activeOrbs = new List<GameObject>();
    private Coroutine spawnOrbs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       spawnOrbs = StartCoroutine(OrbSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OrbSpawner()
    {
        while (true)
        {
            float randomX = Random.Range(horizontalSpawnRange.x, horizontalSpawnRange.y); // range for random horizontal spawn position
            float randomY = Random.Range(verticalSpawnRange.x, verticalSpawnRange.y); // range for random vertical spawn position
            Vector2 orbSpawnPos = new Vector2(randomX, randomY); // exact spawn position
            GameObject newOrb = Instantiate(orbPrefab, orbSpawnPos, Quaternion.identity); // instantiate the new orb
            activeOrbs.Add(newOrb); // add orb to activeOrbs list
            yield return new WaitForSeconds(spawnInterval); // time between each spawn
        }
    }
}
