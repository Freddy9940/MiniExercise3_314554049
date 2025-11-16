using UnityEngine;

[System.Serializable]
public class SpawnEvent
{
	public GameObject prefab;     
	public float delay;                
	public int count = 1;         
}

[CreateAssetMenu(fileName = "WaveSpawnConfig", menuName = "Scriptable Objects/WaveSpawnConfig")]
public class WaveSpawnConfig : ScriptableObject
{
	public SpawnEvent[] spawnEvents;
}
