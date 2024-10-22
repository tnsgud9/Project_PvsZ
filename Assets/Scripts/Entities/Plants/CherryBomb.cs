using Commons;
using UnityEngine;

namespace Entities.Plants
{
    public class CherryBomb : Plant
    {
        public GameObject bombEffect;
        public float radius = 2f;
        public int damage;


        protected override void Start()
        {
            State.CurrentState = AttackingState;
            eventTimer = eventCooldown;
            bombEffect.SetActive(false);
        }

        public override void Attack()
        {
            eventTimer -= Time.deltaTime;
            if (eventTimer <= 0)
            {
                bombEffect.SetActive(true);
                var hitColliders = Physics.OverlapSphere(transform.position, radius);
                foreach (var hit in hitColliders)
                    if (hit.gameObject.layer == LayerMask.NameToLayer("Zombie"))
                    {
                        var unit = hit.GetComponent<Unit>();
                        if (unit is not null) unit.TakeDamage(damage);
                    }

                AudioSource.PlayOneShot(fireAudio);
                State.CurrentState = DyingState;
            }
        }

        public override void StartDie()
        {
            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            StartCoroutine(Logic.WaitThenCallback(3f, () => { Destroy(gameObject); }));
        }
    }
}