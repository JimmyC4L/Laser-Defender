using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    // config params
    [Header("Player Movement")] [FormerlySerializedAs("speed")] [SerializeField]
    private float _moveSpeed = 10f;

    [FormerlySerializedAs("health")] [SerializeField]
    private int _health;

    [Header("Projectile")] [FormerlySerializedAs("laserPrefab")] [SerializeField]
    private GameObject _laserPrefab;

    [FormerlySerializedAs("projectileSpeed")] [SerializeField]
    private float _projectileSpeed;

    [FormerlySerializedAs("projectileFiringPeriod")] [SerializeField]
    private float _projectileFiringPeriod;

    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    private Coroutine _firingCoroutine;

    private void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, _minX, _maxX);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, _minY, _maxY);
        transform.position = new Vector2(newXPosition, newYPosition);
    }


    private void SetUpMoveBoundaries()
    {
        var gameCamera = Camera.main;
        if (gameCamera == null) return;
        _minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        _maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        _minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        _maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            var laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _projectileSpeed);
            yield return new WaitForSeconds(_projectileFiringPeriod);
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firingCoroutine);
        }
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