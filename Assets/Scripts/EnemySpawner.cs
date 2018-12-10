using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [FormerlySerializedAs("waveConfigs")] [SerializeField]
    private List<WaveConfig> _waveConfigs;

    private int startingWave = 0;

    private void Start()
    {
        var currentWave = _waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.NumberOfEnemies; enemyCount++)
        {
            Instantiate(waveConfig.EnemyPrefab, waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
        }
    }
}