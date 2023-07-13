using Fusion;
using UnityEngine;

public class CoinHandler : NetworkBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        var playerHandler = other.gameObject.GetComponent<PlayerHandler>();
        if (playerHandler != null && Object.HasStateAuthority)
        {
            playerHandler.RPC_CollectCoin();
        }
        Runner.Despawn(Object);
    }
}