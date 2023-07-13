using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _movementSpeed = 20.0f;
    [SerializeField] private float _maxSpeed = 10.0f;

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

        var direction = Vector2.zero;
        var newAngle = _rigidBody.rotation;
        
        if (InputManager.IsUp)
        {
            direction = Vector2.up;
            newAngle = 0.0f;
            InputManager.IsUp = false;
        }
        if (InputManager.IsDown)
        {
            direction = Vector2.down;
            newAngle = 180.0f;
            InputManager.IsDown = false;
        }
        if (InputManager.IsLeft)
        {
            direction = Vector2.left;
            newAngle = 90.0f;
            InputManager.IsLeft = false;
        }
        if (InputManager.IsRight)
        {
            direction = Vector2.right;
            newAngle = -90.0f;
            InputManager.IsRight = false;
        }

        _rigidBody.MoveRotation(newAngle);
        _rigidBody.AddForce(direction * _movementSpeed * Runner.DeltaTime);

        if (_rigidBody.velocity.magnitude > _maxSpeed)
        {
            _rigidBody.velocity = _rigidBody.velocity.normalized * _maxSpeed;
        }
    }
}