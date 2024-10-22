using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Commons
{
    public class Logic
    {
        public static T GetOrAddComponent<T>(GameObject gameObject) where T : MonoBehaviour
        {
            // 현재 GameObject에 해당 컴포넌트가 있는지 확인합니다.
            T component = gameObject.GetComponent<T>();

            // 컴포넌트가 없다면 새로 추가합니다.
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
        public static Vector3 RoundVector(Vector3 vector)
        {
            float x = Mathf.Round(vector.x);
            float y = Mathf.Round(vector.y);
            float z = Mathf.Round(vector.z);
            return new Vector3(x, y, z);
        }
        public static Vector3 RoundVectorXZ(Vector3 vector) => new(Mathf.Round(vector.x), vector.y, Mathf.Round(vector.z));
        public static Vector3 RoundVectorXZ(Vector3 vector, Vector3 startRange, Vector3 endRange) => new( 
            Mathf.Clamp(Mathf.Round(vector.x), startRange.x,endRange.x),
            vector.y, 
            Mathf.Clamp(Mathf.Round(vector.z), startRange.z,endRange.z));
        public static Vector3 TruncateVectorXZ(Vector3 vector) => new((int)vector.x, vector.y, (int)vector.z);

        public static IEnumerator WaitThenCallback(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }
    }
    
}