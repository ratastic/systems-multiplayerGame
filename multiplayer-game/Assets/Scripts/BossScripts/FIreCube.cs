using UnityEngine;

public class FireCube : MonoBehaviour
{
    public float life = 5f;
    SpriteRenderer sr;
    float t;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        t = life;
    }

    void Update()
    {
        t -= Time.deltaTime;

        float a = t / life;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);

        if (t <= 0)
            Destroy(gameObject);
    }
}
