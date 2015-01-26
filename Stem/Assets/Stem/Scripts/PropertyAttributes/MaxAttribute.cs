using UnityEngine;


namespace Assets.GGJ2015.Scripts.PropertyAttributes
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
