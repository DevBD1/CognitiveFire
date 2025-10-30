using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OccaSoftware.Ballistics.Editor
{
    public class PropertyDrawerHelpers : PropertyDrawer
    {
        private float propertyHeight;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return propertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();
        }

        public void Initialize()
        {
            propertyHeight = GetDefaultSpacing();
        }

        public Rect GetNewPosition(Rect position)
        {
            float spacing = GetDefaultSpacing();
            position.y = position.y + spacing;
            propertyHeight += spacing;
            return position;
        }

        public float GetDefaultSpacing()
        {
            return EditorGUIUtility.singleLineHeight * 1.1f;
        }

        public Rect GetRect(Rect position)
        {
            return new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        }
    }
}
