using UnityEngine;


namespace Assets.Stem.Scripts.PropertyAttributes 
{
    public class MinAttribute : PropertyAttribute 
    {
        public float Min;

        public MinAttribute(float min) 
        {
            Min = min;
        }
    }
}

