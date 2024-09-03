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

        public static void LogMessage(object sender, string message, bool applyProfile = true, Object context = null)
        {
            LogMessage(sender.GetType().FullName, message, applyProfile);
        }

        public static void LogMessage(string senderName, string message, bool applyProfile = true, Object context = null)
        {
            if (Settings == null) Settings = InsightDebuggerUtils.LoadSettings();

            if (!Settings.DebugModeEnabled) return;

            if (!applyProfile)
            {
                LogUnprofiledMessage(message, context);
                return;
            }

            LogProfiledMessage(senderName, message, context);
        }

        public static void LogWarning(object sender, string message, bool applyProfile = true, Object context = null)
        {
            LogWarning(sender.GetType().FullName, message, applyProfile, context);
        }

        public static void LogWarning(string senderName, string message, bool applyProfile = true, Object context = null)
        {
            if (Settings == null) Settings = InsightDebuggerUtils.LoadSettings();

            if (!Settings.DebugModeEnabled) return;

            if (!applyProfile)
            {
                LogUnprofiledWarning(message, context);
                return;
            }

            LogProfiledWarning(senderName, message, context);
        }

        public static void LogError(object sender, string message, bool applyProfile = true, Object context = null)
        {
            LogError(sender.GetType().FullName, message, applyProfile, context);
        }

        public static void LogError(string senderName, string message, bool applyProfile = true, Object context = null)
        {
            if (Settings == null) Settings = InsightDebuggerUtils.LoadSettings();

            if (!applyProfile)
            {
                LogUnprofiledError(message, context);
                return;
            }

            LogProfiledError(senderName, message, context);
        }

        private static void LogProfiledMessage(string senderName, string message, Object context)
        {
            var profile = InsightDebuggerUtils.GetProfileForSender(senderName, Settings);

            if (profile.GetLogMode() != LogMode.All) return;

            var titleColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetTitleColor());
            var messageColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetMessageColor());

            Debug.Log($"<color={titleColorHtml}>[{senderName}]</color>: <color={messageColorHtml}>{message}</color>", context);
        }

        private static void LogUnprofiledMessage(string message, Object context)
        {
            Debug.Log(message, context);
        }

        private static void LogProfiledWarning(string senderName, string warning, Object context)
        {
            var profile = InsightDebuggerUtils.GetProfileForSender(senderName, Settings);

            if (profile.GetLogMode() == LogMode.ErrorOnly || profile.GetLogMode() == LogMode.None) return;

            var titleColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetTitleColor());
            var warningColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetWarningColor());

            Debug.LogWarning($"<color={titleColorHtml}>[{senderName}]</color>: <color={warningColorHtml}>{warning}</color>", context);
        }

        private static void LogUnprofiledWarning(string warning, Object context)
        {
            Debug.LogWarning(warning, context);
        }

        private static void LogProfiledError(string senderName, string error, Object context)
        {
            var profile = InsightDebuggerUtils.GetProfileForSender(senderName, Settings);

            if (profile.GetLogMode() == LogMode.None) return;

            var titleColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetTitleColor());
            var errorColorHtml = "#" + ColorUtility.ToHtmlStringRGB(profile.GetErrorColor());

            Debug.LogError($"<color={titleColorHtml}>[{senderName}]</color>: <color={errorColorHtml}>{error}</color>", context);
        }

        private static void LogUnprofiledError(string error, Object context)
        {
            Debug.LogError(error, context);
        }
    }
}