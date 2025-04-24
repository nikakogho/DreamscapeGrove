using UnityEngine;
using DreamscapeGrove.Adapters;

namespace DreamscapeGrove.Core
{
    /// <summary>
    /// Polls the current IFocusSource once each frame
    /// </summary>
    public class FocusManager : MonoBehaviour
    {
        public static FocusManager Instance { get; private set; }

        public IFocusSource Source { get; private set; }
        public FocusFrame CurrentFrame { get; private set; }

        public static float FocusThreshold { get; private set; } = 0.70f;
        public static float ConfidenceThreshold { get; private set; } = 0.90f;

        public static void SetFocusThreshold(float focus)
        {
            FocusThreshold = Mathf.Clamp01(focus);
        }

        public static void SetConfidenceThreshold(float confidence)
        {
            ConfidenceThreshold = Mathf.Clamp01(confidence);
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Source = new MockFocusSource(); // later we will choose from UI
            Source.Init();
        }

        private void Update()
        {
            if (Source.TryGetFrame(out var frame)) CurrentFrame = frame;
        }
    }
}
