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

        for (var i = -4; i < 5; ++i)
        {
            for (var j = -4; j < 5; ++j)
            {
                Runner.Spawn(_coinPrefab, new Vector3(i, j), Quaternion.identity);       
            }
        }
    }
}