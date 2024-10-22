using Cysharp.Threading.Tasks;
using Entities.Zombies;
using UnityEngine;

namespace Entities
{
    public class Snowball : Projectile
    {
        [Range(0, 1f)] public float slowPercent = 0.7f;
        public int slowTimeMs = 3000;

        protected override void Start()
        {
            OnCollisionHitCallback = HitCallback;
        }

        private async void HitCallback(Collision collision)
        {
            var zombie = collision.gameObject.GetComponent<Zombie>();
            if (zombie is not null)
            {
                var defaultSpeed = zombie.speed;
                zombie.speed *= slowPercent;
                await UniTask.Delay(slowTimeMs);
                zombie.speed = defaultSpeed;
                // zombie.rimLightMaterial.SetFloat(Zombie.RimPower, 10f);
            }
        }
    }
}