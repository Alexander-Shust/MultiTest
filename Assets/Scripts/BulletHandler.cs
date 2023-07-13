using Fusion;
using UnityEngine;

public class BulletHandler : NetworkBehaviour
{
    [SerializeField] private float _lifeTime = 3.0f;
    [SerializeField] private float _speed = 200.0f;
    
    private TickTimer TimeLeft { get; set; }
    
    [Networked]
    private Vector2 Direction { get; set; }

    public override void Spawned()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }
        
        TimeLeft = TickTimer.CreateFromSeconds(Runner, _lifeTime);
        Direction = transform.forward;
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }
        
        transform.Translate(Direction * _speed * Runner.DeltaTime, Space.World);

        if (TimeLeft.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
    }
}