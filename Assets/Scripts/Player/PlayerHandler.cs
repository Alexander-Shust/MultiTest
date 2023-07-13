using Fusion;
using UnityEngine;

public class PlayerHandler : NetworkBehaviour
{
    [Networked]
    public PlayerRef PlayerRef { get; set; }

    [Networked(OnChanged = nameof(CollectedCoinsChanged))]
    public int CollectedCoins { get; set; }

    [Networked(OnChanged = nameof(HealthChanged))]
    public float Health { get; set; } = 100.0f;

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_CollectCoin()
    {
        ++CollectedCoins;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(float amount)
    {
        Health -= amount;
    }

    private static void CollectedCoinsChanged(Changed<PlayerHandler> changed)
    {
        if (changed.Behaviour.Object.HasInputAuthority)
        {
            UiAccess.Get.SetCoins(changed.Behaviour.CollectedCoins);
        }
    }

    private static void HealthChanged(Changed<PlayerHandler> changed)
    {
        Debug.LogWarning($"Health left: {changed.Behaviour.Health}");
    }
}