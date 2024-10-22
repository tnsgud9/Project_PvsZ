using System.Collections;
using Entities.UnitState;
using UnityEngine;

namespace Entities.Plants
{
    public class Sunflower : Plant
    {
        public GameObject energyPrefab;

        protected override void Start()
        {
            State.CurrentState = new IdleState();
            eventTimer = eventCooldown;
        }

        public override void Idle()
        {
            eventTimer -= Time.deltaTime;
            if (eventTimer <= 0)
            {
                var objDirection = new Vector3(Random.Range(-10f, 10f), Random.Range(0, 360f), Random.Range(-10f, 10f));
                var obj = Instantiate(energyPrefab, transform.position,
                    Quaternion.identity);
                obj.transform.rotation = Quaternion.Euler(objDirection);
                var objRigid = obj.GetComponent<Rigidbody>();
                objRigid.drag = 0;
                objRigid.AddForce(objDirection.normalized * Random.Range(8f, 10f), ForceMode.Impulse);
                // 공격 딜레이 다시 설정
                eventTimer = eventCooldown;
            }
        }
    }
}