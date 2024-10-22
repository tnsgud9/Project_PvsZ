using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities.Plants
{
    public class Spikeweed : Plant
    {
        
        private List<Collider> collidersInTrigger = new List<Collider>();
        
        
        public int attackDamage = 3;
        protected override void Start()
        {
            State.CurrentState = AttackingState;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        public override void Attack()
        {
            eventTimer -= Time.deltaTime;
            var colliders = Physics.OverlapBox(transform.position, transform.lossyScale / 2);
            if (eventTimer <= 0 && colliders.Any(it => it.gameObject.layer == LayerMask.NameToLayer("Zombie")))
            {

                foreach (var collider in colliders)
                {
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
                    {
                        collider.gameObject.GetComponent<Unit>().TakeDamage(attackDamage);
                        
                    }
                    eventTimer = eventCooldown;
                    AudioSource.PlayOneShot(fireAudio);
                }
            }   
        }

        protected void OnTriggerEnter(Collider other)
        {
            // 새로운 충돌 오브젝트 추가
            collidersInTrigger.Add(other);
        }
        protected void OnTriggerExit(Collider other)
        {
            // 충돌이 종료된 오브젝트 제거
            collidersInTrigger.Remove(other);
        }

    }
}