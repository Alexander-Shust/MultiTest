using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, ISpawned
{
    [SerializeField] private GameObject _playerPrefab;

    public void Spawned()
    {
        SpawnPlayer(Runner.LocalPlayer);
    }

    private void SpawnPlayer(PlayerRef player)
    {
        Runner.Spawn(_playerPrefab, new Vector3(player.PlayerId, 0, 0), Quaternion.identity, player);
    }
}