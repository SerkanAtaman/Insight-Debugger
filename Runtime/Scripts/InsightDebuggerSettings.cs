using UnityEngine;

namespace SeroJob.InsightDebugger
{
    public class InsightDebuggerSettings : ScriptableObject
    {
        public bool DebugModeEnabled = false;
        public DefaultDebugProfile DefaultDebugProfile;
        public OverrideDebugProfile[] OverrideDebugProfiles;
    }
}