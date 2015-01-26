#if UNITY_EDITOR
using Assets.Stem.Scripts.PropertyAttributes;
using UnityEditor;
using UnityEngine;


namespace Assets.Stem.Scripts.Editor.PropertyAttributes
{
    [CustomPropertyDrawer(typeof(ReadonlyAttribute))]
    public class ReadonlyAttributePropertyDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            UnityEngine.GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            UnityEngine.GUI.enabled = true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}
#endif
