using System.Linq;
using Commons;
using Entities.Plants;
using JetBrains.Annotations;
using Managers;
using UI;
using UnityEngine;
using static UnityEngine.LayerMask;

namespace Events
{
    public class DeployPlant : InputSystem
    {
        // Variables
        public GameObject plant;
        [CanBeNull] public GameObject obj; // test 인디케이터
        private RaycastHit _hit;
        public AudioClip plantAudio;
        public AudioClip notPlantAudio;
        private AudioSource _audioSource;
        public CardView cardView;

        private Ray _ray;

        // Components
        private Renderer _renderer;

        // TODO: UI에서 식물 선택 기능 필요

        private void Start()
        {
            _renderer = obj.GetComponent<Renderer>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 위치에서 레이 생성

            // TODO: RayCast 단계 함수화
            // RayCast 순서가 중요하다
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, 1 << NameToLayer("Ground")))
            {
                // 충돌한 위치로 오브젝트 이동
                obj.transform.position = _hit.point += Vector3.up * 0.5f;
                _renderer.material.color = Color.red;
            }
            else if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, 1 << NameToLayer("PlantArea")))
            {
                
                obj.SetActive(cardView.selectPlantPrefab is not null);
                // 충돌한 위치로 오브젝트 이동
                obj.transform.position =
                    Logic.RoundVectorXZ(_hit.point + Vector3.up * 0.5f, Const.PlantAreaStartRange,
                        Const.PlantAreaEndRange);

                RaycastHit hit;
                var hitColliders = Physics.OverlapBox(obj.transform.position, obj.transform.lossyScale / 2,
                    Quaternion.identity, 1 << NameToLayer("Plant"));
                                        
                if (hitColliders.Length > 0 && hitColliders.Any(it => !it.gameObject.CompareTag("Projectile")))
                {
                    _renderer.material.color = Color.red;
                }
                else
                {
                    
                    _renderer.material.color = Color.green;

                    if (cardView.selectPlantPrefab is not null)
                    {
                        var needCost = cardView.selectPlantPrefab.GetComponent<Plant>().cost;
                        var calcResult = StageManager.Instance.Energy - needCost;
                        if (0 <= calcResult)
                        {
                            _renderer.material.color = Color.green;
                        }
                        else
                        {
                            _renderer.material.color = Color.red;
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (0 <= calcResult)
                            {
                                _audioSource.PlayOneShot(plantAudio);

                                Instantiate(cardView.selectPlantPrefab,
                                    Logic.RoundVectorXZ(_hit.point + Vector3.up * 0.5f, Const.PlantAreaStartRange,
                                        Const.PlantAreaEndRange),
                                    Quaternion.identity);
                                cardView.toggleGroup.ActiveToggles().First().isOn = false;
                                cardView.selectPlantPrefab = null;
                                StageManager.Instance.Energy -= needCost;
                            }
                            else
                            {
                                _audioSource.PlayOneShot(notPlantAudio);
                            }


                        }
                    }
                }
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(
                Logic.RoundVectorXZ(_hit.point + Vector3.up * 0.5f, Const.PlantAreaStartRange, Const.PlantAreaEndRange),
                obj.transform.lossyScale);
        }
    }
}