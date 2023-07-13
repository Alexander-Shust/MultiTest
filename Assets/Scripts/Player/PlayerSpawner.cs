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
        var playerObject = Runner.Spawn(_playerPrefab, new Vector3(-5 + 10 * player.PlayerId, 0, 0), Quaternion.identity, player);
        playerObject.GetComponent<Renderer>().material.color = player == Runner.LocalPlayer ? Color.red : Color.white;
    }
}