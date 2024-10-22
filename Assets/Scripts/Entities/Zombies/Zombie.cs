using System;
using System.Linq;
using Commons;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.LayerMask;

namespace Entities.Zombies
{
    public class Zombie : Unit
    {
        public static readonly int RimPower = Shader.PropertyToID("_RimPower");
        [Header("Components")] public Material rimLightMaterial;

        [Header("Variables")] public float speed;
        public float animSpeed;
        public int attackDamage;
        public AudioClip dieAudio;

        private Unit _attackTarget;
        private float _defaultSpeed;

        protected Animator Animator;
        protected AudioSource AudioSource;
        protected float DieDownScale = 3f;
        protected bool IgnoreRimLightDependency = false;

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponentInChildren<Animator>();
            AudioSource = GetComponent<AudioSource>();
            if (!IgnoreRimLightDependency)
            {
                rimLightMaterial = new Material(rimLightMaterial); // 인스터스 머터리얼로 재할당 
                var meshRenderers = GetComponentsInChildren<MeshRenderer>();
                foreach (var meshRenderer in meshRenderers)
                    meshRenderer.materials =
                        meshRenderer.materials.Concat(new[] { rimLightMaterial }).ToArray();
                var skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
                    skinnedMeshRenderer.materials =
                        skinnedMeshRenderer.materials.Concat(new[] { rimLightMaterial }).ToArray();
                rimLightMaterial.SetFloat(RimPower, 10f);
            }

            _defaultSpeed = speed;
        }

        protected override void Start()
        {
            State.CurrentState = MovingState;
        }

        //
        // private void OnCollisionEnter(Collision other)
        // {
        //     var unit = other.gameObject.GetComponent<Unit>();
        //     Debug.Log(other.gameObject.name);
        //     if (unit is not null)
        //     {
        //         _attackTarget = unit;
        //         State.CurrentState = AttackingState;
        //     }
        // }

        // private void OnTriggerEnter(Collider other)
        // {
        //     var unit = other.gameObject.GetComponent<Unit>();
        //     Debug.Log(other.gameObject.name);
        //     if (unit is not null)
        //     {
        //         _attackTarget = unit;
        //         State.CurrentState = AttackingState;
        //     }
        // }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (_attackTarget is null && other.gameObject.layer == NameToLayer("Plant") )
            {
                var unit = other.gameObject.GetComponent<Unit>();
                Debug.Log(other.gameObject.name);
                if (unit is not null)
                {
                    _attackTarget = unit;
                    State.CurrentState = AttackingState;
                }
            }
        }


        public override int TakeDamage(int damage)
        {
            rimLightMaterial.SetFloat(RimPower, 1f);
            DOTween.To(
                () => rimLightMaterial.GetFloat(RimPower), // 현재 값 가져오기
                x => rimLightMaterial.SetFloat(RimPower, x), // 값 설정
                10f, // 목표 값
                0.25f // 지속 시간
            ).SetEase(Ease.Linear);
            return base.TakeDamage(damage);
        }

        public override void StartAttack()
        {
            Animator.SetTrigger("Attack");
            eventTimer = 0.1f;
            speed = 0;
        }

        public override void Attack()
        {
            try // Missing Reference를 해결하기 위한 try Catch 비효율적임
            {
                eventTimer -= Time.deltaTime;
                if (eventTimer <= 0)
                {
                    speed = _defaultSpeed;
                    if (_attackTarget.TakeDamage(attackDamage) < 0)
                        _attackTarget = null; // Missing Reference exception을 피하기 위함
                    eventTimer = eventCooldown;
                }
            }
            catch (Exception e)
            {
                State.CurrentState = MovingState;
            }
        }

        public override void EndAttack()
        {
            speed = _defaultSpeed;
            _attackTarget = null;
        }

        public override void StartMove()
        {
            Animator.SetTrigger("Move");
        }

        public override void Move()
        {
            Animator.speed = animSpeed;
            transform.position += transform.forward * (Time.deltaTime * speed);
        }

        public override void StartDie()
        {
            speed = 0;
            Animator.SetTrigger("Die");
            AudioSource.PlayOneShot(dieAudio);
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;

            StartCoroutine(Logic.WaitThenCallback(5f, () => DOTween.To(
                () => transform.position.y,
                y => transform.position += Vector3.down * (0.3f * Time.deltaTime),
                transform.position.y - DieDownScale,
                DieDownScale
            )));
            StartCoroutine(Logic.WaitThenCallback(10f, () => Destroy(gameObject)));
            // TODO 사라지는 효과 실행
        }
    }
}