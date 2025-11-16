using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public WaveSpawnConfig[] waves;   // 一關有多波
    private int currentWave = 0;

    private bool waitingForPlayer = true;

    void Update()
    {
        if (waitingForPlayer && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(StartWave());
        }
    }

    private IEnumerator StartWave()
    {
        waitingForPlayer = false;

        WaveSpawnConfig waveData = waves[currentWave];
        foreach (var evt in waveData.spawnEvents)
        {
            for (int i = 0; i < evt.count; i++)
            {
                Instantiate(evt.prefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(evt.delay);
            }
        }

        currentWave++;

        if (currentWave < waves.Length)
        {
            waitingForPlayer = true;
        }
        else
        {
            Debug.Log("Wave spawned");
        }
    }
}
