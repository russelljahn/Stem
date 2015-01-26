#if UNITY_EDITOR
using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.PropertyAttributes;
using UnityEditor;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Editor.PropertyAttributes
{
    [CustomPropertyDrawer(typeof(MaxAttribute))]
    public class MaxAttributeDrawer : PropertyDrawer 
    {
        private MaxAttribute _attributeValue;
        private MaxAttribute AttributeValue 
        { 
            get { return _attributeValue.IsNotNull() ? _attributeValue : (_attributeValue = (MaxAttribute)attribute); }
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            property.floatValue = Mathf.Min(AttributeValue.Max, property.floatValue);
            EditorGUI.PropertyField(position, property);
        }
    }
}
#endif
