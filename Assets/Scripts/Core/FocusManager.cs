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

        private void Awake()
        {
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
