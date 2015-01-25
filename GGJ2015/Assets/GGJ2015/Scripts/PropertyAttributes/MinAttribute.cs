using UnityEngine;


namespace Assets.GGJ2015.Scripts.PropertyAttributes 
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

