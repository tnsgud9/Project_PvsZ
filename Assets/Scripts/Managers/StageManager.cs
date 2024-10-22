using System;
using System.Collections.Generic;
using Collections;
using Enums;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class StageManager : DestoryableSingleton<StageManager>
    {
        
        [Serializable]
        public struct ZombieSpawnInfo {
            public GameObject zombiePrefab;
            public float appearanceTime;
        }
        public List<GameObject> plantSets;
        //public List<GameObject> zombieSets;
        private AudioSource _audioSource;
        public AudioClip gameStartAudio;
        public AudioClip musicAudio;
        public List<ZombieSpawnInfo> zombieSets;
        public float endTime;
        public float timer = 0f;

        public bool gameInProgress;

        [field: SerializeField] private int energy;

        public Text energyText;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.PlayOneShot(gameStartAudio);
            _audioSource.clip = musicAudio;
            _audioSource.PlayDelayed(5f);
        }

        private void Start()
        {
            EventBus<GameEventType>.Subscribe(GameEventType.GameClear, () =>
            {
                GetComponent<ZombieSpawner>().enabled = false;
                GetComponent<EnergySpawner>().enabled = false;
                _audioSource.Stop();
            });
            EventBus<GameEventType>.Subscribe(GameEventType.GameOver, () =>
            {
                GetComponent<ZombieSpawner>().enabled = false;
                GetComponent<EnergySpawner>().enabled = false;
                _audioSource.Stop();
            });
        }

        public int Energy
        {
            get => energy;
            set
            {
                
                energy = value;
                energyText.text = energy.ToString();
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (Physics.Raycast(new Vector3(-9f, 0.5f, -5f), Vector3.forward, out RaycastHit hitInfo, 10f,
                    1 << LayerMask.NameToLayer("Zombie")))
            {
                if (!hitInfo.collider.CompareTag("Projectile"))
                {
                    EventBus<GameEventType>.Publish(GameEventType.GameOver);
                }
            }

            if (timer >= endTime)
            {
                Debug.Log("Game Clear");
                EventBus<GameEventType>.Publish(GameEventType.GameClear);
            }
        }
    }
}