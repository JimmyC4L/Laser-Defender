using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [FormerlySerializedAs("waveConfigs")] [SerializeField]
    private List<WaveConfig> _waveConfigs;

    [FormerlySerializedAs("StartingWave")] [SerializeField]
    private int _startingWave = 0;

    [FormerlySerializedAs("loopWave")] [SerializeField]
    private bool _loopWave = false;

    public IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (_loopWave);
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.NumberOfEnemies; enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.EnemyPrefab, waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveCount = _startingWave; waveCount < _waveConfigs.Count; waveCount++)
        {
            var currentWave = _waveConfigs[waveCount];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}