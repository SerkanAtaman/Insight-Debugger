using UnityEditor;
using UnityEngine;

namespace SeroJob.InsightDebugger.Editor
{
    public static class InsightDebuggerEditor
    {
        [MenuItem("SeroJob/InsightDebugger/Window", priority = 1)]
        public static void ShowWindow()
        {
            InsightDebuggerWindow wnd = EditorWindow.GetWindow<InsightDebuggerWindow>();
            wnd.titleContent = new GUIContent("Insight Debugger");
        }

        [MenuItem("SeroJob/InsightDebugger/EnableDebugMode", priority = 2)]
        public static void EnableDebugMode()
        {
            var settings = InsightDebuggerUtils.LoadSettings();
            settings.DebugModeEnabled = true;

            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssetIfDirty(settings);
        }

        [MenuItem("SeroJob/InsightDebugger/EnableDebugMode", true, priority = 2)]
        public static bool IsDebugModeEnabled()
        {
            var settings = InsightDebuggerUtils.LoadSettings();
            return !settings.DebugModeEnabled;
        }

        [MenuItem("SeroJob/InsightDebugger/DisableDebugMode", priority = 3)]
        public static void DisableDebugMode()
        {
            var settings = InsightDebuggerUtils.LoadSettings();
            settings.DebugModeEnabled = false;

            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssetIfDirty(settings);
        }

        [MenuItem("SeroJob/InsightDebugger/DisableDebugMode", true, priority = 3)]
        public static bool IsDebugModeDisabled()
        {
            var settings = InsightDebuggerUtils.LoadSettings();
            return settings.DebugModeEnabled;
        }
    }
}