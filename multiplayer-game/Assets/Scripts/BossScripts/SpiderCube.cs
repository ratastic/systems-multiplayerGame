using UnityEngine;
using System.Collections;

public class SpiderCube : MonoBehaviour
{
    public GameObject hatchCubePrefab;
    public float fadeTime = 0.4f;
    bool hatched = false;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (hatched) return;
        hatched = true;

        // hatch mini cube
        Instantiate(hatchCubePrefab, transform.position, Quaternion.identity);

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float t = fadeTime;

        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = t / fadeTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
            yield return null;
        }

        Destroy(gameObject);
    }
}
