using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyPath : MonoBehaviour
{
    private WaveConfig _waveConfig;

    [FormerlySerializedAs("waypoints")] [SerializeField]
    private List<Transform> _waypoints;

    private int _waypointIndex = 0;

    private void Start()
    {
        _waypoints = _waveConfig.GetWaypoints();
        transform.position = _waypoints[_waypointIndex].transform.position;
    }

    private void Update()
    {
        MoveOnPath();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        _waveConfig = waveConfig;
    }

    private void MoveOnPath()
    {
        if (_waypointIndex <= _waypoints.Count - 1)
        {
            var targetPosition = _waypoints[_waypointIndex].transform.position;
            var movementOfThisFrame = _waveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementOfThisFrame);

            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
}