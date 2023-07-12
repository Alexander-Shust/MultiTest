using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, ISpawned
{
    [SerializeField] private GameObject _playerPrefab;
    
    private void OnEnable()
    {
        Debug.LogWarning("Started!");
    }

    public void Spawned()
    {
        Debug.LogWarning("Spawned!");
    }

    private void SpawnPlayer(PlayerRef player)
    {
        
    }
}