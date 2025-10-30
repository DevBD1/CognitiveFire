using UnityEngine;
using UnityEditor;
using OccaSoftware.Ballistics.Runtime;

namespace OccaSoftware.Ballistics.Editor
{
    [CustomPropertyDrawer(typeof(Projectile))]
    public class ProjectileDrawer : PropertyDrawerHelpers
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.LabelField(GetRect(position), new GUIContent("Projectile Configuration"), EditorStyles.boldLabel);

            EditorGUI.indentLevel++;


            position = GetNewPosition(position);
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("MuzzleVelocity"), new GUIContent("Muzzle Velocity (m/s)"));


            position = GetNewPosition(position);
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("BallisticCoefficient"), new GUIContent("Ballistic Coefficient (kg/m^2)"));


            position = GetNewPosition(position);
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("CrossSectionalArea"), new GUIContent("Cross Sectional Area (m^2)"));


            position = GetNewPosition(position);
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("ProjectileMass"), new GUIContent("Projectile Mass (kg)"));


            position = GetNewPosition(position);
            EditorGUI.LabelField(GetRect(position), "Initial Kinetic Energy (J)", $"{property.FindPropertyRelative("InitialKineticEnergy").floatValue:0.00}");


            EditorGUI.indentLevel--;


            EditorGUI.EndProperty();
        }
    }
}
