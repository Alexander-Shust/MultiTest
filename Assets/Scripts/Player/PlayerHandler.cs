using Fusion;

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
        if (amount >= Health)
        {
            Health = 0.0f;
            UiAccess.Get.AddResult(PlayerRef, CollectedCoins);
            Runner.Despawn(Object);
        }
        else
        {
            Health -= amount;   
        }
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
        if (changed.Behaviour.Object.HasInputAuthority)
        {
            UiAccess.Get.SetHealth(changed.Behaviour.Health);
        }
    }
}