using System.Collections;
using Commons;
using UnityEngine;
using static UnityEngine.LayerMask;

namespace Entities.Plants
{
    public class LaserShooter : Plant
    {
        public int attackDamage=2;

        protected override void Start()
        {
            State.CurrentState = AttackingState;
        }

        public override void Attack()
        {
            eventTimer -= Time.deltaTime;
            var distance = Vector3.Distance(Vector3.right * transform.position.x,
                Vector3.right * Const.DetectRayDistance);

            if (Physics.Raycast(transform.position, Vector3.right, distance, 1 << NameToLayer("Zombie")))
            {
                if (eventTimer <= 0)
                {
                    RaycastHit[] hits =
                        Physics.RaycastAll(transform.position, Vector3.right, distance, 1 << NameToLayer("Zombie"));
                    foreach (var hit in hits)
                    {
                        hit.collider.gameObject.GetComponent<Unit>().TakeDamage(attackDamage);
                    }
                    AudioSource.PlayOneShot(fireAudio);

                    eventTimer = eventCooldown;
                    
                }
                
                Debug.DrawRay(transform.position, Vector3.right * distance, Color.red);
            }
            else
            {
                // state.CurrentState = new IdleState();
                Debug.DrawRay(transform.position, Vector3.right * distance, Color.green);
            }
        }

    }
}