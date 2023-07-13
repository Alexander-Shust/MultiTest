using Fusion;

public class PlayerHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(CollectedCoinsChanged))]
    public int CollectedCoins { get; set; } = 0;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_CollectCoin()
    {
        ++CollectedCoins;
    }

    private static void CollectedCoinsChanged(Changed<PlayerHandler> changed)
    {
        UiAccess.Get.SetCoins(changed.Behaviour.CollectedCoins);
    }
}