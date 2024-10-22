using UnityEngine;

namespace Entities.Zombies
{
    public class SinZombie : Zombie
    {
        public float amplitude = 2f; // 진폭
        public float frequency = 1f; // 주기

        private Vector3 _startPosition;

        private void OnEnable()
        {
            _startPosition = transform.position;
        }

        public override void Move()
        {
            // 시간에 따른 Sin 함수를 사용해 좌우로 이동하는 값을 계산
            var xOffset = Mathf.Sin(Time.time * frequency) * amplitude;

            // 전방 방향으로 전진
            var forwardMovement = transform.forward * speed * Time.deltaTime;

            // 좌우 이동 (transform.right 방향으로 Sin 함수 적용)
            var sidewaysMovement = transform.right * xOffset;

            // 새로운 위치로 객체 이동 (forward + sideways 이동)
            transform.position += forwardMovement + sidewaysMovement;
            Animator.speed = animSpeed;
        }
    }
}