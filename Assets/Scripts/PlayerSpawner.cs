using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, ISpawned
{
    private void OnEnable()
    {
        Debug.LogWarning("Started!");
    }

    public void Spawned()
    {
        Debug.LogWarning("Spawned!");
    }
}