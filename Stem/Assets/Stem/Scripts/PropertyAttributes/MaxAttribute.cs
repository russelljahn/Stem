using UnityEngine;


namespace Assets.Stem.Scripts.PropertyAttributes
{
    public class MaxAttribute : PropertyAttribute
    {
        public float Max;

        public MaxAttribute(float max)
        {
            Max = max;
        }
    }
}
