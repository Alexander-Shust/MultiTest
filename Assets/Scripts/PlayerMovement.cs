using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _movementSpeed = 20.0f;
    [SerializeField] private float _rotationSpeed = 90.0f;

    private Rigidbody2D _rigidBody;

    public override void Spawned()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }

        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }
        
        if (InputManager.IsUp)
        {
            var force = Vector2.up * _movementSpeed * Runner.DeltaTime;
            _rigidBody.AddForce(force);
            InputManager.IsUp = false;
        }

        if (InputManager.IsDown)
        {
            var force = Vector2.down * _movementSpeed * Runner.DeltaTime;
            _rigidBody.AddForce(force);
            InputManager.IsDown = false;
        }
        
        if (InputManager.IsLeft)
        {
            var force = Vector2.left * _movementSpeed * Runner.DeltaTime;
            _rigidBody.AddForce(force);
            InputManager.IsLeft = false;
        }

        if (InputManager.IsRight)
        {
            var force = Vector2.right * _movementSpeed * Runner.DeltaTime;
            _rigidBody.AddForce(force);
            InputManager.IsRight = false;
        }
    }
}