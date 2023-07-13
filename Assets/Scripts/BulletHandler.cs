using Fusion;
using UnityEngine;

public class BulletHandler : NetworkBehaviour
{
    [SerializeField] private float _lifeTime = 3.0f;
    [SerializeField] private float _speed = 200.0f;
    
    private TickTimer TimeLeft { get; set; }

    private Rigidbody2D _rigidBody;
    
    [Networked]
    public Vector2 Direction { get; set; }
    
    [Networked]
    public PlayerRef Originator { get; set; }

    public override void Spawned()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }
        
        _rigidBody = GetComponent<Rigidbody2D>();
        TimeLeft = TickTimer.CreateFromSeconds(Runner, _lifeTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }
        
        _rigidBody.MovePosition(_rigidBody.position + Direction * _speed * Runner.DeltaTime);

        if (TimeLeft.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || other.GetComponent<PlayerHandler>().PlayerRef == Originator)
        {
            return;
        }
        
        Debug.LogWarning("Player hit!");
        Runner.Despawn(Object);
    }
}