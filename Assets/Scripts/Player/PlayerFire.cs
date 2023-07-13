using Fusion;
using UnityEngine;

namespace Player
{
    public class PlayerFire : NetworkBehaviour
    {
        [SerializeField] private float _shotDelay = 0.3f;
        [SerializeField] private GameObject _bulletPrefab;

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

                var bullet = Runner.Spawn(_bulletPrefab, _rigidBody.position, Quaternion.identity, Object.InputAuthority);
                bullet.transform.Rotate(0, 0, _rigidBody.rotation);
                FireCooldown = TickTimer.CreateFromSeconds(Runner, _shotDelay);
            }
        }
    }
}