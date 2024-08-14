using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Utility.Editor.Tools
{
    public class SceneSwitcher : EditorWindow
    {
        [MenuItem("Tools/Scene Switcher")]
        public static void ShowWindow()
        {
            GetWindow<SceneSwitcher>("Scene Switcher");
        }

        void OnGUI()
        {
            GUILayout.Label("Scene Switcher", EditorStyles.boldLabel);

            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (GUILayout.Button("Open " + System.IO.Path.GetFileNameWithoutExtension(scene.path)))
                {
                    OpenScene(scene.path);
                }
            }
        }

        void OpenScene(string scenePath)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }
    }
}
