using BadJuja.Player;
using UnityEditor;
using UnityEngine;

namespace BadJuja
{
    [CustomEditor(typeof(CircleDistribution))]
    public class CircleDistributionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CircleDistribution circleDistribution = (CircleDistribution)target;
            if (GUILayout.Button("Activate Next Object"))
            {
                circleDistribution.ActivateNextObject();
            }
        }
    }
}
