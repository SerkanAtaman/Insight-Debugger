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
            if (!applyProfile)
            {
                LogUnprofiledMessage(message);
                return;
            }

            LogProfiledMessage(sender, message);
        }

        private static void LogProfiledMessage(object sender, string message)
        {
            if (Settings == null) Settings = InsightDebuggerUtils.LoadSettings();

            var senderName = sender.GetType().FullName;
            var profile = InsightDebuggerUtils.GetProfileForSender(senderName, Settings);

            var titleColorHtml = "#" + ColorUtility.ToHtmlStringRGBA(profile.GetTitleColor());
            var messageColorHtml = "#" + ColorUtility.ToHtmlStringRGBA(profile.GetMessageColor());

            Debug.Log($"<color={titleColorHtml}>[{senderName}]</color>: <color={messageColorHtml}>{message}</color>");
        }

        private static void LogUnprofiledMessage(string message)
        {
            Debug.Log(message);
        }

        private static void LogProfiledWarning(object sender, string warning)
        {
            if (Settings == null) Settings = InsightDebuggerUtils.LoadSettings();

            var senderName = sender.GetType().FullName;
            var profile = InsightDebuggerUtils.GetProfileForSender(senderName, Settings);

            var titleColorHtml = "#" + ColorUtility.ToHtmlStringRGBA(profile.GetTitleColor());
            var warningColorHtml = "#" + ColorUtility.ToHtmlStringRGBA(profile.GetWarningColor());

            Debug.Log($"<color={titleColorHtml}>[{senderName}]</color>: <color={warningColorHtml}>{warning}</color>");
        }

        private static void LogUnprofiledWarning(string warning)
        {
            Debug.LogWarning(warning);
        }

        private static void LogProfiledError(object sender, string error)
        {
            if (Settings == null) Settings = InsightDebuggerUtils.LoadSettings();

            var senderName = sender.GetType().FullName;
            var profile = InsightDebuggerUtils.GetProfileForSender(senderName, Settings);

            var titleColorHtml = "#" + ColorUtility.ToHtmlStringRGBA(profile.GetTitleColor());
            var errorColorHtml = "#" + ColorUtility.ToHtmlStringRGBA(profile.GetErrorColor());

            Debug.Log($"<color={titleColorHtml}>[{senderName}]</color>: <color={errorColorHtml}>{error}</color>");
        }

        private static void LogUnprofiledError(string error)
        {
            Debug.LogError(error);
        }
    }
}