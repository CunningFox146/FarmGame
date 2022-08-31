using UnityEngine;

namespace Farm.Util
{
    public static class VectorUtil
    {
        public static Vector3 GetDirection(float angle)
        {
            return new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)).normalized;
        }

        public static Vector3 GetRandomDirection(float radius, float angleMin = 0f, float angleMax = Mathf.PI * 2f)
        {
            float angle = Random.Range(angleMin, angleMax);
            return GetDirection(angle) * radius;
        }
    }
}
