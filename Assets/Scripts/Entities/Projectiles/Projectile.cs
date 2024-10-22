using System;
using Commons;
using UnityEngine;

namespace Entities
{
    public class Projectile : MonoBehaviour
    {
        public float speed;
        public int damage;
        public Color Color = Color.green;
        public Vector3 direction = Vector3.right;
        public Action<Collision> OnCollisionHitCallback;
        public Action<Collider> OnTriggerHitCallback;

        public AudioSource audioSource;
        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        protected virtual void Start()
        {
            GetComponent<MeshRenderer>().material.color = Color;
        }

        protected virtual void Update()
        {
            transform.position += direction * (Time.deltaTime * speed);

            if (transform.position.x < -Const.DetectRayDistance ||
                transform.position.x > Const.DetectRayDistance + 1f ||
                transform.position.z < -Const.DetectRayDistance ||
                transform.position.z > Const.DetectRayDistance + 1f)
                Destroy(gameObject);
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            var unit = other.gameObject.GetComponent<Unit>();
            if (unit is not null)
            {
                other.gameObject.GetComponent<Unit>().TakeDamage(damage);
                OnCollisionHitCallback?.Invoke(other);
            }

            // Debug.Log(other.gameObject.name);
            DisableDelete();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var unit = other.gameObject.GetComponent<Unit>();
            if (unit is not null)
            {
                other.gameObject.GetComponent<Unit>().TakeDamage(damage);
                OnTriggerHitCallback?.Invoke(other);
            }

            // Debug.Log(other.gameObject.name);
            DisableDelete();
        }

        void DisableDelete()
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            audioSource.Play();
            StartCoroutine(Logic.WaitThenCallback(1f, () => Destroy(gameObject)));
        }
    }
}