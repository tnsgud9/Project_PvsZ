using System;
using Commons;
using Managers;
using UnityEngine;
using static UnityEngine.LayerMask;

namespace Events
{
    public class PickupEnergy : InputSystem
    {
        private AudioSource _audioSource;
        public AudioClip getEnergyAudio;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 위치에서 레이 생성
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << NameToLayer("Energy")))
            {
                Destroy(hit.transform.gameObject);
                StageManager.Instance.Energy += Const.EarnEnergy;
                _audioSource.PlayOneShot(getEnergyAudio);

                //energyText.text = StageManager.Instance.energy.ToString();
            }
        }
    }
}