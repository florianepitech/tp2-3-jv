using System;
using System.Collections.Generic;
using UnityEngine;

public class VfxPool : MonoBehaviour
{
    public ParticleSystem startGameEffect;
    public ParticleSystem endGameEffect;

    public int POOL_SIZE = 10;
    private List<ParticleSystem> _particleSystemsPool = new();
    
    private void Start()
    {
        for (var i = 0; i < POOL_SIZE; i++)
        {
            // Créer une instance du prefab VFX
            var particleSystem = Instantiate(startGameEffect, Vector3.zero, Quaternion.identity);
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
        }
    }

    // Public methods
    
    public void SpawnStartGame(Vector3 position)
    {
        if (startGameEffect != null)
        {
            // Créer une instance du prefab VFX à la position spécifiée
            var ps = GetParticleInPool(startGameEffect, position);
            ps.Play();
        }
    }
    
    public void SpawnEndGame(Vector3 position)
    {
        if (endGameEffect != null)
        {
            // Créer une instance du prefab VFX à la position spécifiée
            var ps = GetParticleInPool(endGameEffect, position);
            ps.Play();
        }
    }
    
    // Private methods
    
    private ParticleSystem GetParticleInPool(ParticleSystem particleSystem, Vector3 position)
    {
        foreach (var ps in _particleSystemsPool)
        {
            if (!ps.gameObject.activeInHierarchy)
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