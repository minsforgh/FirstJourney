using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Utility.Editor.Tools
{
    public class PrefabRandomizer2D : EditorWindow
    {
        public GameObject[] prefabs;
        public int numberOfPrefabs = 10;
        public float areaWidth = 10f;
        public float areaHeight = 10f;
        public Transform parent;

        private List<GameObject> instantiatedPrefabs = new List<GameObject>();

        SerializedObject serializedObject;
        SerializedProperty prefabsProperty;

        [MenuItem("Tools/Prefab Randomizer 2D")]
        public static void ShowWindow()
        {
            GetWindow<PrefabRandomizer2D>("Prefab Randomizer 2D");
        }

        void OnEnable()
        {
            serializedObject = new SerializedObject(this);
            prefabsProperty = serializedObject.FindProperty("prefabs");
        }

        void OnGUI()
        {
            serializedObject.Update();

            GUILayout.Label("Prefab Randomizer 2D", EditorStyles.boldLabel);

            parent = EditorGUILayout.ObjectField("Parent", parent, typeof(Transform), true) as Transform;
            numberOfPrefabs = EditorGUILayout.IntField("Number of Prefabs", numberOfPrefabs);
            areaWidth = EditorGUILayout.FloatField("Area Width", areaWidth);
            areaHeight = EditorGUILayout.FloatField("Area Height", areaHeight);


            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Prefabs");
            EditorGUILayout.PropertyField(prefabsProperty, true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Randomize Prefabs"))
            {
                RandomizePrefabs();
            }

            if (GUILayout.Button("Clear Prefabs"))
            {
                ClearPrefabs();
            }
        }

        private void ClearPrefabs()
        {
            foreach (GameObject prefab in instantiatedPrefabs)
            {
                DestroyImmediate(prefab);
            }
            instantiatedPrefabs.Clear();
        }

        void RandomizePrefabs()
        {
            if (prefabs == null || prefabs.Length == 0)
            {
                Debug.LogWarning("No prefabs assigned.");
                return;
            }

            for (int i = 0; i < numberOfPrefabs; i++)
            {
                Vector2 randomPosition = new Vector2(
                    Random.Range(-areaWidth / 2f, areaWidth / 2f),
                    Random.Range(-areaHeight / 2f, areaHeight / 2f)
                );

                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

                if (parent == null)
                {
                    parent = new GameObject("Randomized Prefabs").transform;
                }
                // only for 2D environment
                GameObject instantiatedPrefab = Instantiate(prefab, parent.transform.position + new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity, parent);
                instantiatedPrefabs.Add(instantiatedPrefab);
            }
        }
    }
}
