using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class PunchGround : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;
    [SerializeField] ParticleSystem impactParticles = null;

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("battleGround"))
        {
            CamRumbleManager.instance.CamRumble(impulseSource);
            PlayParticles();
            Debug.Log("boss punched ground");
        }
    }

    public void PlayParticles()
    {
        Debug.Log("particles released");
        impactParticles.Play();
    }
}
