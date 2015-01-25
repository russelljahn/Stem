#if UNITY_EDITOR
using Assets.GGJ2015.Scripts.PropertyAttributes;
using UnityEditor;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Editor.PropertyAttributes
{
    [CustomPropertyDrawer(typeof(Clamp01Attribute))]
    public class Clamp01AttributeDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            var newValue = EditorGUI.Slider(position, property.name, property.floatValue, 0f, 1f);
            property.floatValue = Mathf.Clamp01(newValue);
        }
    }
}
#endif
