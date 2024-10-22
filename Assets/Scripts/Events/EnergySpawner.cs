using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Events
{
    public class EnergySpawner : MonoBehaviour
    {
        public GameObject energyPrefab;
        public Vector3 spawnArea;
        public float energySpawnDelay=10f;
        public float increaseSpawnDelay=5f;
        
        private IEnumerator _spawnEnergy;

        public void Awake()
        {
            _spawnEnergy = SpawnEnergy();
        }

        public void OnEnable()
        {
            StartCoroutine(_spawnEnergy);
        }

        public void OnDisable()
        {
            StopCoroutine(_spawnEnergy);
        }


        private IEnumerator SpawnEnergy()
        {
            while (true)
            {
                yield return new WaitForSeconds(energySpawnDelay);

                var spawnPosition = new Vector3(Random.Range(spawnArea.x, -spawnArea.x),spawnArea.y,Random.Range(spawnArea.z, -spawnArea.z));

                var obj = Instantiate(energyPrefab, spawnPosition,Quaternion.identity);
                obj.GetComponent<Rigidbody>().drag = Random.Range(5f, 15f);
            }
        }
    }
}
