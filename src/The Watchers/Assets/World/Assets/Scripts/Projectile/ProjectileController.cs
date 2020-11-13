using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private bool HasPlayed { get; set; }
    public ParticleSystem particle;
    private ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        explosion = particle.subEmitters.GetSubEmitterSystem(0);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (HasPlayed) return;
        HasPlayed = true;
        particle.Stop();
        particle.TriggerSubEmitter(0);
        Debug.Log("TEst");
    }

}