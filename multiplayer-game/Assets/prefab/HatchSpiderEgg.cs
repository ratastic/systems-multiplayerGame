using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HatchSpiderEgg : MonoBehaviour
{
    [SerializeField] ParticleSystem spiderBabies = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("battleGround"))
        {
            spiderBabies.Play();
            Debug.Log("egg hit ground and hatching");
        }
    }
}
