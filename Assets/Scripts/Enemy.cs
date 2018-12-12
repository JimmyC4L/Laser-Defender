using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{   
    [Header("Enemy config")]
    [FormerlySerializedAs("health")] [SerializeField]
    private float _health = 100;

    [FormerlySerializedAs("shotCounter")] [SerializeField]
    private float _shotCounter;

    [FormerlySerializedAs("minTimeBetweenShots")] [SerializeField]
    private float _minTimeBetweenShots = 1f;

    [FormerlySerializedAs("maxTimeBetweenShots")] [SerializeField]
    private float _maxTimeBetweenShots = 5f;

    [FormerlySerializedAs("projectile")] [SerializeField]
    private GameObject _projectile;

    [FormerlySerializedAs("projectileSpeed")] [SerializeField]
    private float _projectileSpeed = 10f;
    
    [Header("Sound")]
    [FormerlySerializedAs("deathVFX")] [SerializeField]
    private GameObject _deathVfx;

    [FormerlySerializedAs("deathVFXDuration")] [SerializeField]
    private float _deathVfxDuration = 1f;

    [FormerlySerializedAs("deathSFX")] [SerializeField]
    private AudioClip _deathSfx;

    [FormerlySerializedAs("deathSfxVolume")] [SerializeField]
    [Range(0,1)]
    private float _deathSfxVolume = 0.7f;

    private void Start()
    {
        ResetShotCounter();
    }

    private void ResetShotCounter()
    {
        _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndFire();
    }

    private void CountDownAndFire()
    {
        _shotCounter -= Time.deltaTime;
        if (_shotCounter <= 0)
        {
            Fire();
            ResetShotCounter();
        }
    }

    private void Fire()
    {
        var laser = Instantiate(_projectile, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.Damage;
        damageDealer.DestroyOnHit();
        DestroyOnZeroHealth();
    }

    private void DestroyOnZeroHealth()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
            if (Camera.main != null) AudioSource.PlayClipAtPoint(_deathSfx, Camera.main.transform.position, _deathSfxVolume);
            var deathVfx = Instantiate(_deathVfx, transform.position, Quaternion.identity);
            Destroy(deathVfx, _deathVfxDuration);
        }
    }
}