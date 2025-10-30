using UnityEngine;
using UnityEditor;
using OccaSoftware.Ballistics.Runtime;

namespace OccaSoftware.Ballistics.Editor
{
    [CustomPropertyDrawer(typeof(Environment))]
    public class EnvironmentDrawer : PropertyDrawerHelpers
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            string s;

            EditorGUI.BeginProperty(position, label, property);


            EditorGUI.LabelField(GetRect(position), new GUIContent("Environment"), EditorStyles.boldLabel);
            

            EditorGUI.indentLevel++;


            position = GetNewPosition(position);
            EditorGUI.LabelField(GetRect(position), "Gravity", EditorStyles.boldLabel);
            
            
            position = GetNewPosition(position);
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("Gravity"), new GUIContent("Gravity (m/s^2)"));


            position = GetNewPosition(position);
            EditorGUI.LabelField(GetRect(position), "Atmospheric Conditions", EditorStyles.boldLabel);


            position = GetNewPosition(position);
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("RelativeHumidity"), new GUIContent("Relative Humidity (%)"));


            position = GetNewPosition(position);
            s = "Temperature (" + "\u00B0" + "C)";
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("Temperature"), new GUIContent(s));


            position = GetNewPosition(position);
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("AirPressureAtmospheres"), new GUIContent("Air Pressure (atm)"));


            position = GetNewPosition(position);
            s = $"Calculated Air Density (kg/m^3)";
            EditorGUI.LabelField(GetRect(position), s, $"{property.FindPropertyRelative("AirDensity").floatValue:0.0000}");


            position = GetNewPosition(position);
            s = $"Calculated Speed of Sound (m/s)";
            EditorGUI.LabelField(GetRect(position), s, $"{property.FindPropertyRelative("SpeedOfSound").floatValue:0.00}");


            EditorGUI.indentLevel--;


            EditorGUI.EndProperty();
        }


    }
}
