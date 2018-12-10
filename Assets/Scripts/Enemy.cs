using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
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
        }
    }
}