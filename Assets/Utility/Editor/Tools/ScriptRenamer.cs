using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace Utility.Editor.Tools
{
    public class ScriptRenamer : EditorWindow
    {
        public MonoScript scriptToRename;
        public string newClassName;

        private string originalClassName;
        private string originalFilePath;

        SerializedObject serializedObject;

        [MenuItem("Tools/Script Renamer")]
        public static void ShowWindow()
        {
            GetWindow<ScriptRenamer>("Script Renamer");
        }

        private void OnEnable()
        {
            serializedObject = new SerializedObject(this);
        }

        void OnGUI()
        {
            GUILayout.Label("Script Class & File Renamer", EditorStyles.boldLabel);

            scriptToRename = (MonoScript)EditorGUILayout.ObjectField("Script to Rename", scriptToRename, typeof(MonoScript), false);
            newClassName = EditorGUILayout.TextField("New Class Name", newClassName);

            if (scriptToRename != null && !string.IsNullOrEmpty(newClassName))
            {
                if (GUILayout.Button("Rename Script"))
                {
                    if (EditorUtility.DisplayDialog("Confirm Rename",
                        $"Are you sure you want to rename {scriptToRename.name} to {newClassName}?", "Rename", "Cancel"))
                    {
                        RenameScript();
                    }
                }
            }
        }

        private void RenameScript()
        {
            originalClassName = scriptToRename.name;
            originalFilePath = AssetDatabase.GetAssetPath(scriptToRename);

            if (ClassOrFileNameExists(newClassName))
            {
                Debug.LogError($"The class or file name '{newClassName}' already exists. Choose a different name.");
                return;
            }

            // Update class name inside the script
            string scriptText = File.ReadAllText(originalFilePath);
            scriptText = scriptText.Replace($"class {originalClassName}", $"class {newClassName}");
            File.WriteAllText(originalFilePath, scriptText);

            // Rename the script file
            string newFilePath = Path.GetDirectoryName(originalFilePath) + "/" + newClassName + ".cs";
            AssetDatabase.RenameAsset(originalFilePath, newClassName);
            AssetDatabase.Refresh();

            // Update references in the project
            UpdateClassReferences(originalClassName, newClassName);

            Debug.Log($"Renamed {originalClassName} to {newClassName}, and updated references.");
        }

        private bool ClassOrFileNameExists(string className)
        {
            return AssetDatabase.FindAssets(className).Length > 0 || System.AppDomain.CurrentDomain.GetAssemblies()
                   .SelectMany(a => a.GetTypes()).Any(t => t.Name == className);
        }

        private void UpdateClassReferences(string oldClassName, string newClassName)
        {
            // Allow user to select a specific folder
            string folder = EditorUtility.OpenFolderPanel("Select Folder to Update", Application.dataPath, "");
            if (string.IsNullOrEmpty(folder)) return;

            // Ensure the folder path is within the Assets folder
            folder = folder.Replace(Application.dataPath, "Assets");

            // Update all scripts in the selected folder
            string[] allScriptPaths = Directory.GetFiles(folder, "*.cs", SearchOption.AllDirectories);
            foreach (string scriptPath in allScriptPaths)
            {
                string fileContent = File.ReadAllText(scriptPath);
                if (fileContent.Contains(oldClassName))
                {
                    fileContent = fileContent.Replace(oldClassName, newClassName);
                    File.WriteAllText(scriptPath, fileContent);
                }
            }

            // Update prefab references in the selected folder
            string[] allPrefabPaths = Directory.GetFiles(folder, "*.prefab", SearchOption.AllDirectories);
            foreach (string prefabPath in allPrefabPaths)
            {
                string fileContent = File.ReadAllText(prefabPath);
                if (fileContent.Contains(oldClassName))
                {
                    fileContent = fileContent.Replace(oldClassName, newClassName);
                    File.WriteAllText(prefabPath, fileContent);
                }
            }

            // Update scene references in the selected folder
            string[] allScenePaths = Directory.GetFiles(folder, "*.unity", SearchOption.AllDirectories);
            foreach (string scenePath in allScenePaths)
            {
                string fileContent = File.ReadAllText(scenePath);
                if (fileContent.Contains(oldClassName))
                {
                    fileContent = fileContent.Replace(oldClassName, newClassName);
                    File.WriteAllText(scenePath, fileContent);
                }
            }

            AssetDatabase.Refresh();
        }
    }
}