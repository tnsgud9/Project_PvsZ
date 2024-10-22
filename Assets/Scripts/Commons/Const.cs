using UnityEngine;

namespace Commons
{
    public static class Const
    {
        public const int EarnEnergy = 50;
        public const float DetectRayDistance = 9f;

        public static readonly Vector3 PlantAreaStartRange = new(-8f, 0, -4f);
        public static readonly Vector3 PlantAreaEndRange = new(8f, 0, 3f);
    }
}