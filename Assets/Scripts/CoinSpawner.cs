using Fusion;
using UnityEngine;

public class CoinSpawner : SimulationBehaviour, ISpawned
{
    [SerializeField] private GameObject _coinPrefab;
    
    public void Spawned()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }
        Debug.LogWarning("Spawn coin");
        Runner.Spawn(_coinPrefab, new Vector3(2, 2), Quaternion.identity);
    }
}