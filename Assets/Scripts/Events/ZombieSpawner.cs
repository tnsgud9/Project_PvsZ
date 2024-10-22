using System;
using System.Collections;
using System.Linq;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Events
{
    public class ZombieSpawner : MonoBehaviour
    {
        public float zombieSpawnStartDelay = 20f;
        public float zombieSpawnDelayRangeMin = 3f;
        public float zombieSpawnDelayRangeMax = 15f;
        public float zombieSpawnDelayReduction = 0.01f;
        
        private float _timer = 0f;
        private AudioSource _audioSource;
        public AudioClip zombieComingAudio;
        public AudioClip zombieBurningAudio;


        public int minSpawnLine = -4;
        public int maxSpawnLine = 3;

        private IEnumerator spawnZombie;

        public void Awake()
        {
            spawnZombie = SpawnZombie();
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnEnable()
        {
            StartCoroutine(spawnZombie);
        }

        public void OnDisable()
        {
            StopCoroutine(spawnZombie);
        }

        public void Update()
        {
            _timer += Time.deltaTime;
        }

        private IEnumerator SpawnZombie()
        {
            yield return new WaitForSeconds(zombieSpawnStartDelay);
            _audioSource.PlayOneShot(zombieComingAudio);

            while (true)
            {
                var zombieSpawnDelay = Random.Range(zombieSpawnDelayRangeMin, zombieSpawnDelayRangeMax);
                yield return new WaitForSeconds(zombieSpawnDelay);
                float randomLine = Random.Range(minSpawnLine, maxSpawnLine);
                var zombieSets = StageManager.Instance.zombieSets.Where(it => it.appearanceTime <= _timer).ToList();
                var randomIndex = Random.Range(0, zombieSets.Count);
                Debug.Log($"Zombie : {randomIndex}");
                var selectedZombie = zombieSets[randomIndex].zombiePrefab;

                var spawnPosition = new Vector3(12.85f, 0.5f, randomLine);
                var zombieObj = Instantiate(selectedZombie, spawnPosition, Quaternion.Euler(0, -90, 0));
                //zombieSpawnDelay = zombieSpawnDelay - decreaseDelay <= 0 ? 1f : zombieSpawnDelay - decreaseDelay; 
                zombieSpawnDelayRangeMax -= zombieSpawnDelayReduction;
                zombieSpawnDelayRangeMax = zombieSpawnDelayRangeMax <= zombieSpawnDelayRangeMin + 1f
                    ? zombieSpawnDelayRangeMin + 1f
                    : zombieSpawnDelayRangeMax;
            }
        }
    }
}