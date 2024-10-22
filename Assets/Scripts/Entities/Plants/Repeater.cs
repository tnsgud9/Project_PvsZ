using System.Collections;
using Commons;
using UnityEngine;
using static UnityEngine.LayerMask;

namespace Entities.Plants
{
    public class Repeater : Plant
    {
        public int projectileCount = 2;
        public float projectileDelay = 0.1f;

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
                Debug.DrawRay(transform.position, Vector3.right * distance, Color.red);
                if (eventTimer <= 0)
                {
                    StartCoroutine(GenerateProjectile());
                    eventTimer = eventCooldown;
                }
            }
            else
            {
                // state.CurrentState = new IdleState();
                Debug.DrawRay(transform.position, Vector3.right * distance, Color.green);
            }
        }

        private IEnumerator GenerateProjectile()
        {
            for (var i = 0; i < projectileCount; i++)
            {
                var pt = Instantiate(projectile, transform.position, Quaternion.identity);
                pt.layer = gameObject.layer;
                AudioSource.PlayOneShot(fireAudio);
                yield return new WaitForSeconds(projectileDelay);
            }
        }
    }
}