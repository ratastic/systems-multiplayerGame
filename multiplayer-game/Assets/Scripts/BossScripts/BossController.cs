using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Fire Radius Settings")]
    public GameObject fireCubePrefab;
    public int fireSegments = 20;
    public float fireRadius = 2f;
    public float fireCubeLifetime = 2f;
    public float fireExpandAmount = 1f;

    [Header("Spider Rain Settings")]
    public GameObject spiderPrefab;
    public int spiderCount = 5;
    public float spiderSpawnHeight = 5f;
    public float spiderSpread = 1.5f;

    [Header("Glow Settings")]
    public SpriteRenderer bossSprite;
    public float glowDuration = 0.5f;

    private void Start()
    {
        StartCoroutine(BossLoop());
    }

    IEnumerator BossLoop()
    {
        while (true)
        {
            // fire radius
            yield return StartCoroutine(GlowRed());
            yield return StartCoroutine(FireRadiusBurst());
            yield return new WaitForSeconds(2f);

            //spider rain
            yield return StartCoroutine(GlowRed());
            yield return StartCoroutine(SpiderRain());
            yield return new WaitForSeconds(2f);
        }
    }

    //visual cue for boss attacking 

    IEnumerator GlowRed()
    {
        if (bossSprite != null)
        {
            bossSprite.color = Color.red;
            yield return new WaitForSeconds(glowDuration);
            bossSprite.color = Color.white;
        }
    }

    //fire radius
    IEnumerator FireRadiusBurst()
    {
        if (fireCubePrefab == null)
        {
            Debug.LogError("FireCube prefab missing!");
            yield break;
        }

        for (int i = 0; i < fireSegments; i++)
        {
            float angle = i * Mathf.PI * 2f / fireSegments;
            Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

            Vector3 startPos = transform.position + dir * fireRadius;

            GameObject cube = Instantiate(fireCubePrefab, startPos, Quaternion.identity);

            // Make it expand outward naturally
            StartCoroutine(ExpandAndDie(cube, dir));
        }

        yield return null;
    }

    IEnumerator ExpandAndDie(GameObject cube, Vector3 direction)
    {
        float t = 0f;

        Vector3 initialPos = cube.transform.position;

        while (t < 1f)
        {
            if (cube == null) yield break;

            cube.transform.position = initialPos + direction * (t * fireExpandAmount);
            t += Time.deltaTime;
            yield return null;
        }

        Destroy(cube);
    }

    // spider eggz
    IEnumerator SpiderRain()
    {
        if (spiderPrefab == null)
        {
            Debug.LogError("Spider prefab missing!");
            yield break;
        }

        for (int i = 0; i < spiderCount; i++)
        {
            Vector3 pos = transform.position +
                          new Vector3(Random.Range(-spiderSpread, spiderSpread), spiderSpawnHeight, 0);

            GameObject spider = Instantiate(spiderPrefab, pos, Quaternion.identity);

            Rigidbody2D rb = spider.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1.2f;

            Destroy(spider, 4f); // cleanup
            yield return new WaitForSeconds(0.2f);
        }
    }
}
