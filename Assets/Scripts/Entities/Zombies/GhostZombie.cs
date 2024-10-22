using UnityEngine;

namespace Entities.Zombies
{
    public class GhostZombie : Zombie
    {
        protected override void Awake()
        {
            IgnoreRimLightDependency = true;
            base.Awake();
        }

        protected override void Start()
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
            State.CurrentState = MovingState;
            DieDownScale = 10f;
        }

        protected override void OnTriggerStay(Collider other)
        {
        }

        public override void StartAttack()
        {
        }

        public override void Attack()
        {
        }
    }
}