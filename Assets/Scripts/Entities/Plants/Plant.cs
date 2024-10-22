using System.Linq;
using Commons;
using UnityEngine;
using static UnityEngine.LayerMask;

namespace Entities.Plants
{
    public class Plant : Unit
    {
        public GameObject projectile;
        public AudioClip fireAudio;
        public int cost;
        public Color testColor;
        protected AudioSource AudioSource;

        protected override void Awake()
        {
            base.Awake();
            AudioSource = GetComponent<AudioSource>();
            GetComponentInChildren<SkinnedMeshRenderer>().materials.First().color = testColor;
        }

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
                    Debug.DrawRay(transform.position, Vector3.right * distance, Color.cyan);
                    // 이벤트 발생
                    var pt = Instantiate(projectile, transform.position, Quaternion.identity);
                    pt.layer = gameObject.layer;
                    AudioSource.PlayOneShot(fireAudio);
                    // 공격 딜레이 다시 설정
                    eventTimer = eventCooldown;
                }
            }
            else
            {
                // state.CurrentState = new IdleState();
                Debug.DrawRay(transform.position, Vector3.right * distance, Color.green);
            }
        }
    }
}