using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfig : ScriptableObject
{
    [FormerlySerializedAs("enemyPrefab")] [SerializeField]
    private GameObject _enemyPrefab;

    [FormerlySerializedAs("pathPrefab")] [SerializeField]
    private GameObject _pathPrefab;

    [FormerlySerializedAs("timeBetweenSpawns")] [SerializeField]
    private float _timeBetweenSpawns = 0.5f;

    [FormerlySerializedAs("spawnRandomFactor")] [SerializeField]
    private float _spawnRandomFactor = 0.3f;

    [FormerlySerializedAs("numberOfEnemies")] [SerializeField]
    private int _numberOfEnemies = 5;

    [FormerlySerializedAs("moveSpeed")] [SerializeField]
    private float _moveSpeed = 2f;
    
    public GameObject EnemyPrefab
    {
        get { return _enemyPrefab; }
    }

    public GameObject PathPrefab
    {
        get { return _pathPrefab; }
    }

    public float TimeBetweenSpawns
    {
        get { return _timeBetweenSpawns; }
    }

    public float SpawnRandomFactor
    {
        get { return _spawnRandomFactor; }
    }

    public int NumberOfEnemies
    {
        get { return _numberOfEnemies; }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
    }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform wayPoints in _pathPrefab.transform)
        {
            waveWaypoints.Add(wayPoints);
        }
        return waveWaypoints;
    }
    
}