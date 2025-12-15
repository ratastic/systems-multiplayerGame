using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossController : MonoBehaviour
{
    private Animator bossAnim;

    [Header("Fire Radius Settings")]
    public GameObject fireCubePrefab;
    public int fireSegments = 20;
    public float fireRadius = 25f;
    public float fireCubeLifetime = 10f;
    public float fireExpandAmount = 15f;

    [Header("Spider Rain Settings")]
    public GameObject spiderPrefab;
    public int spiderCount = 5;
    public float spiderSpawnHeight = 5f;
    public float spiderSpread = 1.5f;
    public Sprite[] eggSprites = new Sprite[2];
    public SpriteRenderer eggSR;

    [Header("Glow Settings")]
    public float glowDuration = 0.5f;
    public SpriteRenderer bossSprite;
    private Color originalBossColor;

    // BOSS STATE MACHINE
    public enum SpiderState { Begin, Idle, FireRadius, SpiderRain, PunchGround, Death }
    private SpiderState currentSpiderState;
    private float timer;
    private float timeNeededToWait;
    private float randomIdleLength;
    private bool coroutineStarted = false;
    private int previousState = -1;
    private int currentState = -1;

    private void Start()
    {
        bossAnim = GetComponent<Animator>();

        if (bossSprite != null)
        {
            originalBossColor = bossSprite.color;
        }
       
        currentSpiderState = SpiderState.Begin;
        timer = 0;
        timeNeededToWait = 5.0f;
        randomIdleLength = Random.Range(5f, 8f);
        //StartCoroutine(BossLoop());
    }

    // IEnumerator BossLoop()
    // {
    //     while (true)
    //     {
    //         // fire radius
    //         yield return StartCoroutine(GlowRed());
    //         yield return StartCoroutine(FireRadiusBurst());
    //         yield return new WaitForSeconds(2f);

    //         //spider rain
    //         yield return StartCoroutine(GlowRed());
    //         yield return StartCoroutine(SpiderRain());
    //         yield return new WaitForSeconds(2f);
    //     }
    // }

    private void Update()
    {
        timer += Time.deltaTime;
        switch (currentSpiderState) // SWITCH BETWEEN IDLE STATE, ATTACK STATES, AND DEFEATED STATE
        {
            case SpiderState.Begin:
                Emerge();
                break;
            case SpiderState.Idle: 
                if (!coroutineStarted)
                {
                    coroutineStarted = true;
                    StartCoroutine(GlowRed());
                }
                Idle();
                break;
            case SpiderState.FireRadius:
                if (!coroutineStarted)
                {
                    coroutineStarted = true;
                    StartCoroutine(FireRadiusBurst());
                }
                Fire();
                break;
            case SpiderState.SpiderRain:
                if (!coroutineStarted)
                {
                    coroutineStarted = true;
                    StartCoroutine(RainSpiders());
                }
                Rain();
                break;
            case SpiderState.PunchGround:
                Punch();
                break;
            case SpiderState.Death:
                Death();
                break;
        }
    }

    private SpiderState RandomAttack() // RANDOM ATTACK PLAYED AFTER IDLE STATE / GRACE PERIOD
    {
        int r = Random.Range(1, 5);
        while (previousState == currentState && r == currentState)
        {
            r = Random.Range(1, 4);
        }

        previousState = currentState;
        currentState = r;

        return (SpiderState)r;
    }

    private void Emerge()
    {
        if (timer >= timeNeededToWait)
        {
            timer = 0.0f;
            currentSpiderState = SpiderState.Idle;
            timeNeededToWait = 5f;
        }
    }

    private void Idle() // RETURNS TO IDLE STATE AFTER EVERY ATTACK
    {
        bossAnim.SetBool("isIdle", true);
        // call idle animation here
        if (timer >= timeNeededToWait)
        {
            timer = 0.0f;
            coroutineStarted = false;
            currentSpiderState = RandomAttack();
            timeNeededToWait = 1f;
        }
    }

    private void Fire()
    {
        if (timer >= timeNeededToWait)
        {
            timer = 0.0f;
            coroutineStarted = false;
            currentSpiderState = SpiderState.Idle;
            timeNeededToWait = randomIdleLength;
        }
    }

    private void Rain()
    {
        if (timer >= timeNeededToWait)
        {
            timer = 0.0f;
            coroutineStarted = false;
            currentSpiderState = SpiderState.Idle;
            timeNeededToWait = randomIdleLength;
        }
    }

    private void Punch()
    {
        bossAnim.SetBool("isPunching", true);
        Debug.Log("punch state");
        if (timer >= timeNeededToWait)
        {
            timer = 0.0f;
            coroutineStarted = false;
            bossAnim.SetBool("isPunching", false);
            currentSpiderState = SpiderState.Idle;
            timeNeededToWait = randomIdleLength;
        }
    }

    private void Death()
    {
        Debug.Log("players win :-)");
    }


    // COROUTINES F0R THE DIFFERENT SPIDER STATES
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
            Debug.Log("fire is spreading");
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
            t += Time.deltaTime;
            cube.transform.position = initialPos + direction * (t * fireExpandAmount);
            yield return null;
        }

        Destroy(cube);
    }

    // spider eggz
    IEnumerator RainSpiders()
    {
        if (spiderPrefab == null)
        {
            Debug.LogError("Spider prefab missing!");
            yield break;
        }

        for (int i = 0; i < spiderCount; i++)
        {
            Debug.Log("it's raining spiders aaa");
            Vector3 pos = transform.position + new Vector3(Random.Range(-spiderSpread, spiderSpread), spiderSpawnHeight, 0);

            int index = Random.Range(0, eggSprites.Length);
            Debug.Log("egg sprite index: " + index);
            eggSR.sprite = eggSprites[index];

            GameObject spider = Instantiate(spiderPrefab, pos, Quaternion.identity);

            Rigidbody2D rb = spider.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1.2f;

            Destroy(spider, 4f); // cleanup
            yield return new WaitForSeconds(0.2f);
        }
    }

    //visual cue for boss attacking 
    IEnumerator GlowRed()
    {
        Debug.Log("boss will attack");
        if (bossSprite != null)
        {
            yield return new WaitForSeconds(timeNeededToWait - 1f);
            bossSprite.color = Color.red;
            yield return new WaitForSeconds(glowDuration);
            bossSprite.color = originalBossColor;
        }
    }
}
