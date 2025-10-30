using UnityEngine;
using UnityEditor;
using OccaSoftware.Ballistics.Runtime;

namespace OccaSoftware.Ballistics.Editor
{
    [CustomPropertyDrawer(typeof(SimulationConfig))]
    public class SimulationStateDrawer : PropertyDrawerHelpers
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.LabelField(GetRect(position), new GUIContent("Simulation Settings"), EditorStyles.boldLabel);

            EditorGUI.indentLevel++;

            
            position = GetNewPosition(position);
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("MaxSimulationDistance"), new GUIContent("Max Simulation Distance (m)"));


            position = GetNewPosition(position);
            EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("UpdateFrequency"), new GUIContent("Simulation Update Timing"));

            if((SimulationConfig.SimUpdateFrequency)property.FindPropertyRelative("UpdateFrequency").enumValueIndex == SimulationConfig.SimUpdateFrequency.CustomTiming)
            {
                position = GetNewPosition(position);
                EditorGUI.PropertyField(GetRect(position), property.FindPropertyRelative("CustomUpdateFrequency"), new GUIContent("Custom Update Frequency Timing (s)"));
            }


            EditorGUI.indentLevel--;


            EditorGUI.EndProperty();
        }


    }
}
