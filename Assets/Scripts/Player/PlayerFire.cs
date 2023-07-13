using Fusion;
using UnityEngine;

namespace Player
{
    public class PlayerFire : NetworkBehaviour
    {
        [SerializeField] private float _shotDelay = 0.3f;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _gunPosition;

        private Rigidbody2D _rigidBody;
        
        [Networked]
        private TickTimer FireCooldown { get; set; }

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

            if (InputManager.IsFire)
            {
                InputManager.IsFire = false;
                if (!FireCooldown.ExpiredOrNotRunning(Runner))
                {
                    return;
                }

                var bullet = Runner.Spawn(_bulletPrefab, _gunPosition.position, Quaternion.identity, Object.InputAuthority);
                var handler = bullet.GetComponent<BulletHandler>();
                handler.Direction = ((Vector2)_gunPosition.position - _rigidBody.position).normalized;
                handler.Originator = Object.InputAuthority;
                FireCooldown = TickTimer.CreateFromSeconds(Runner, _shotDelay);
            }
        }
    }
}