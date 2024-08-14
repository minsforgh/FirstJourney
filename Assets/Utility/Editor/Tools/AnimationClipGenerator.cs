using UnityEngine;
using UnityEditor;

namespace Utility.Editor.Tools
{
    public class AnimationClipGenerator : EditorWindow
    {
        public Sprite[] sprites;
        public float frameRate = 12f;
        public string animationName = "New Animation";

        [MenuItem("Tools/Animation Clip Generator")]
        public static void ShowWindow()
        {
            GetWindow<AnimationClipGenerator>("Animation Clip Generator");
        }

        void OnGUI()
        {
            animationName = EditorGUILayout.TextField("Animation Name", animationName);
            frameRate = EditorGUILayout.FloatField("Frame Rate", frameRate);
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty spritesProperty = serializedObject.FindProperty("sprites");
            EditorGUILayout.PropertyField(spritesProperty, true);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Generate Animation Clip"))
            {
                GenerateAnimationClip();
            }
        }

        void GenerateAnimationClip()
        {
            if (sprites == null || sprites.Length == 0)
            {
                Debug.LogWarning("No sprites assigned.");
                return;
            }

            AnimationClip clip = new AnimationClip();
            clip.frameRate = frameRate;

            EditorCurveBinding spriteBinding = new EditorCurveBinding
            {
                type = typeof(SpriteRenderer),
                path = "",
                propertyName = "m_Sprite"
            };

            ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[sprites.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                keyframes[i] = new ObjectReferenceKeyframe
                {
                    time = i / frameRate,
                    value = sprites[i]
                };
            }

            AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, keyframes);
            AssetDatabase.CreateAsset(clip, $"Assets/{animationName}.anim");
            AssetDatabase.SaveAssets();

            Debug.Log($"Animation clip '{animationName}' created with {sprites.Length} frames.");
        }
    }
}