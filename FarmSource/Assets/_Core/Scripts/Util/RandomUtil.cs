using UnityEngine;

namespace Farm.Util
{
    public static class RandomUtil
    {
        public static bool RandomBool(float chance = 0.5f)
        {
            return Random.Range(0f, 1f) <= chance;
        }

        public static float RandomSign(float chance = 0.5f)
        {
            return RandomBool(chance) ? 1f : -1f;
        }
    }
}
