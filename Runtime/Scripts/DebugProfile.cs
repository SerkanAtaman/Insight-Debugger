using UnityEngine;

namespace SeroJob.InsightDebugger
{
    public interface IDebugProfile
    {
        public Color GetTitleColor();
        public Color GetMessageColor();
        public Color GetWarningColor();
        public Color GetErrorColor();
    }

    [System.Serializable]
    public class DefaultDebugProfile : IDebugProfile
    {
        public Color TitleColor = Color.cyan;
        public Color MessageColor = Color.white;
        public Color WarningColor = Color.white;
        public Color ErrorColor = Color.white;

        public Color GetTitleColor() => TitleColor;
        public Color GetMessageColor() => MessageColor;
        public Color GetWarningColor() => WarningColor;
        public Color GetErrorColor() => ErrorColor;
    }

    [System.Serializable]
    public class OverrideDebugProfile : IDebugProfile
    {
        public string SenderFullName;
        public Color TitleColor = Color.cyan;
        public Color MessageColor = Color.white;
        public Color WarningColor = Color.white;
        public Color ErrorColor = Color.white;

        public Color GetTitleColor() => TitleColor;
        public Color GetMessageColor() => MessageColor;
        public Color GetWarningColor() => WarningColor;
        public Color GetErrorColor() => ErrorColor;
    }
}