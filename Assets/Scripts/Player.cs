using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    // config params
    [FormerlySerializedAs("speed")] [SerializeField]
    private float _moveSpeed = 10f;

    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    private void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
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
}