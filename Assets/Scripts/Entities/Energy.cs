using System;
using Commons;
using Managers;
using UnityEngine;

namespace Entities
{
    public class Energy : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("PickUP Energy");
            StageManager.Instance.Energy += Const.EarnEnergy;
        }
    }
}