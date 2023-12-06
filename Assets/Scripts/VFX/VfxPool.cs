using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VfxPool : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    public int POOL_SIZE = 10;
    private List<ParticleSystem> _particleSystemsPool = new();
    
    private void Start()
    {
        for (var i = 0; i < POOL_SIZE; i++)
        {
            // Créer une instance du prefab VFX
            var particleSystem = Instantiate(ParticleSystem, Vector3.zero, Quaternion.identity);
            particleSystem.gameObject.SetActive(false);
            // Ajouter l'instance au pool
            _particleSystemsPool.Add(particleSystem);
        }
    }
    
    private void FixedUpdate()
    {
        for (var i = 0; i < _particleSystemsPool.Count; i++)
        {
            var ps = _particleSystemsPool[i];
            // Verifier si la particule est terminée
            if (ps.IsAlive())
            {
                continue;
            }
            // Si terminée, la désactiver
            ps.gameObject.SetActive(false);
        }
    }

    // Public methods
    
    public void Spawn(Vector3 position)
    {
        if (ParticleSystem != null)
        {
            // Créer une instance du prefab VFX à la position spécifiée
            var ps = GetParticleInPool(ParticleSystem, position);
            // Set the position of the particle system
            ps.transform.position = position;
            ps.gameObject.SetActive(true);
            ps.Play();
        }
    }
    
    // Private methods
    
    private ParticleSystem GetParticleInPool(ParticleSystem particleSystem, Vector3 position)
    {
        foreach (var ps in _particleSystemsPool)
        {
            if (!ps.gameObject.activeSelf)
            {
                ps.gameObject.SetActive(true);
                return ps;
            }
        }
        // Si aucun inactif, en créer un nouveau et l'ajouter à la pool
        ParticleSystem newParticle = Instantiate(particleSystem, position, Quaternion.identity);
        _particleSystemsPool.Add(newParticle);
        return newParticle;
    }
    
}