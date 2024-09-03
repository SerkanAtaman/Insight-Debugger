using UnityEngine;

namespace SeroJob.InsightDebugger
{
    public static class InsightDebuggerUtils
    {
        public static IDebugProfile GetProfileForSender(object sender, InsightDebuggerSettings settings)
        {
            if(DebugProfilesContainsSender(sender.GetType().FullName, settings.OverrideDebugProfiles, out var resultProfile))
            {
                return resultProfile;
            }

            return settings.DefaultDebugProfile;
        }

        public static IDebugProfile GetProfileForSender(string senderFullName, InsightDebuggerSettings settings)
        {
            if (DebugProfilesContainsSender(senderFullName, settings.OverrideDebugProfiles, out var resultProfile))
            {
                return resultProfile;
            }

            return settings.DefaultDebugProfile;
        }

        public static bool DebugProfilesContainsSender(string senderFullName, OverrideDebugProfile[] profiles, out OverrideDebugProfile resultProfile)
        {
            resultProfile = null;
            bool result = false;

            foreach (var profile in profiles)
            {
                if(string.Equals(senderFullName, profile.SenderFullName))
                {
                    result = true;
                    resultProfile = profile;
                    break;
                }
            }

            return result;
        }

        public static InsightDebuggerSettings LoadSettings()
        {
            var path = "InsightDebugger/InsightDebuggerSettings";

            return Resources.Load<InsightDebuggerSettings>(path);
        }
    }
}