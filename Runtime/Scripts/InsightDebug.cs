using UnityEngine;

namespace SeroJob.InsightDebugger
{
    public static class InsightDebug
    {
        public static InsightDebuggerSettings Settings { get; private set; }

        static InsightDebug()
        {
            Settings = null;
        }

        public static void LogMessage(object sender, string message, bool applyProfile = true)
        {
            LogMessage(sender.GetType().FullName, message, applyProfile);
        }

        public static void LogMessage(string senderName, string message, bool applyProfile = true)
        {
            if (Settings == null) Settings = InsightDebuggerUtils.LoadSettings();

            if (!Settings.DebugModeEnabled) return;

            if (!applyProfile)
            {
                LogUnprofiledMessage(message);
                return;
            }

            LogProfiledMessage(senderName, message);
        }

        public static void LogWarning(object sender, string message, bool applyProfile = true)
        {
            LogWarning(sender.GetType().FullName, message, applyProfile);
        }

        public static void LogWarning(string senderName, string message, bool applyProfile = true)
        {
            if (Settings == null) Settings = InsightDebuggerUtils.LoadSettings();

            if (!Settings.DebugModeEnabled) return;

            if (!applyProfile)
            {
                LogUnprofiledWarning(message);
                return;
            }

            LogProfiledWarning(senderName, message);
        }

        public static void LogError(object sender, string message, bool applyProfile = true)
        {
            LogError(sender.GetType().FullName, message, applyProfile);
        }

        public static void LogError(string senderName, string message, bool applyProfile = true)
        {
            if (Settings == null) Settings = InsightDebuggerUtils.LoadSettings();

            if (!applyProfile)
            {
                LogUnprofiledError(message);
                return;
            }

            LogProfiledError(senderName, message);
        }

        private static void LogProfiledMessage(string senderName, string message)
        {
            var profile = InsightDebuggerUtils.GetProfileForSender(senderName, Settings);

            if (profile.GetLogMode() != LogMode.All) return;

            var titleColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetTitleColor());
            var messageColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetMessageColor());

            Debug.Log($"<color={titleColorHtml}>[{senderName}]</color>: <color={messageColorHtml}>{message}</color>");
        }

        private static void LogUnprofiledMessage(string message)
        {
            Debug.Log(message);
        }

        private static void LogProfiledWarning(string senderName, string warning)
        {
            var profile = InsightDebuggerUtils.GetProfileForSender(senderName, Settings);

            if (profile.GetLogMode() == LogMode.ErrorOnly || profile.GetLogMode() == LogMode.None) return;

            var titleColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetTitleColor());
            var warningColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetWarningColor());

            Debug.LogWarning($"<color={titleColorHtml}>[{senderName}]</color>: <color={warningColorHtml}>{warning}</color>");
        }

        private static void LogUnprofiledWarning(string warning)
        {
            Debug.LogWarning(warning);
        }

        private static void LogProfiledError(string senderName, string error)
        {
            var profile = InsightDebuggerUtils.GetProfileForSender(senderName, Settings);

            if (profile.GetLogMode() == LogMode.None) return;

            var titleColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetTitleColor());
            var errorColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetErrorColor());

            Debug.LogError($"<color={titleColorHtml}>[{senderName}]</color>: <color={errorColorHtml}>{error}</color>");
        }

        private static void LogUnprofiledError(string error)
        {
            Debug.LogError(error);
        }
    }
}