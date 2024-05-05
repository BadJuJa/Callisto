using UnityEditor;
using UnityEngine;

namespace BadJuja.UI
{
    [CustomEditor(typeof(LoadUIScene))]
    public class LoadUISceneEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LoadUIScene loadUIScene = (LoadUIScene)target;
            if (GUILayout.Button("Load UI Scene"))
            {
                loadUIScene.Load();
            }

            if (GUILayout.Button("Unload UI Scene"))
            {
                loadUIScene.Unload();
            }
        }
    }
}
