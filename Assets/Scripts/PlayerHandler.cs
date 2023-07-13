using Fusion;

public class PlayerHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(CollectedCoinsChanged))]
    public int CollectedCoins { get; set; }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_CollectCoin()
    {
        ++CollectedCoins;
    }

    private static void CollectedCoinsChanged(Changed<PlayerHandler> changed)
    {
        if (changed.Behaviour.Object.HasInputAuthority)
        {
            UiAccess.Get.SetCoins(changed.Behaviour.CollectedCoins);
        }
    }
}