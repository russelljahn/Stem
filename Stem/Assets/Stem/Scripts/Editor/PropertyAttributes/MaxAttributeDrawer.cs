#if UNITY_EDITOR
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.PropertyAttributes;
using UnityEditor;
using UnityEngine;


namespace Assets.Stem.Scripts.Editor.PropertyAttributes
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
