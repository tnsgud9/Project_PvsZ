using UnityEngine;

namespace Entities.Zombies
{
    public class BombZombie : Zombie
    {
        public float radius = 2f;
        public AudioClip bombAudio;

        public override void StartAttack()
        {
            speed = 0;
            var hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hit in hitColliders)
            {
                var unit = hit.GetComponent<Unit>();
                if (unit is not null) unit.TakeDamage(attackDamage);
            }

            State.CurrentState = DyingState;
        }
    }
}