using System.IO;
using UnityEditor;
using UnityEngine;

namespace SeroJob.InsightDebugger.Editor
{
    public class InsightDebuggerWindow : EditorWindow
    {
        [SerializeField] private InsightDebuggerSettings _settings;

        private SerializedObject _serializedSettingsObject;
        
        public void CreateGUI()
        {
            _settings = GetSettings();
            _serializedSettingsObject = new SerializedObject(_settings);
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(_serializedSettingsObject.FindProperty("DefaultDebugProfile"), new GUIContent("Default Profile", "The Default Profile"));

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(_serializedSettingsObject.FindProperty("OverrideDebugProfiles"), new GUIContent("Override Profiles", "The Override Profiles"));

            _serializedSettingsObject.ApplyModifiedProperties();

            GUILayout.EndHorizontal();
        }

        private InsightDebuggerSettings GetSettings()
        {
            var path = "Assets/Resources/InsightDebugger/InsightDebuggerSettings.asset";
            var directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            var asset = AssetDatabase.LoadAssetAtPath<InsightDebuggerSettings>(path);

            if(asset == null)
            {
                return CreateSettingsAsset();
            }

            return asset;
        }

        private InsightDebuggerSettings CreateSettingsAsset()
        {
            var path = "Assets/Resources/InsightDebugger/InsightDebuggerSettings.asset";
            var directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            var asset = ScriptableObject.CreateInstance<InsightDebuggerSettings>();
            asset.DefaultDebugProfile = new DefaultDebugProfile();
            asset.OverrideDebugProfiles = new OverrideDebugProfile[0];
            asset.DebugModeEnabled = false;

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return AssetDatabase.LoadAssetAtPath<InsightDebuggerSettings>(path);
        }
    }
}