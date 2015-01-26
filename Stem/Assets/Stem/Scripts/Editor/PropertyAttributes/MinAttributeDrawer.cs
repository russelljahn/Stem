#if UNITY_EDITOR
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.PropertyAttributes;
using UnityEditor;
using UnityEngine;


namespace Assets.Stem.Scripts.Editor.PropertyAttributes
{
    [CustomPropertyDrawer(typeof(MinAttribute))]
    public class MinAttributeDrawer : PropertyDrawer 
    {
        private MinAttribute _attributeValue;
        private MinAttribute AttributeValue {
            get { return _attributeValue.IsNotNull() ? _attributeValue : (_attributeValue = (MinAttribute)attribute); }
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            property.floatValue = Mathf.Max(AttributeValue.Min, property.floatValue);
            EditorGUI.PropertyField(position, property);
        }
    }
}
#endif
