using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [FormerlySerializedAs("health")] [SerializeField]
    private float _health = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.Damage;
        DestroyOnZeroHealth();
    }

    private void DestroyOnZeroHealth()
    {
        if (_health <= 0) 
        {
            Destroy(gameObject);
        }
    }
}