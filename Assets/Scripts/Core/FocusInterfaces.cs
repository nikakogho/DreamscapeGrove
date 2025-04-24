using UnityEngine;

namespace DreamscapeGrove.Core
{
    public struct FocusFrame
    {
        public double timestamp;

        [Range(0.0f, 1.0f)]
        public float focus;

        [Range(0.0f, 1.0f)]
        public float confidence;
    }

    public interface IFocusSource
    {
        string Name { get; }
        void Init();
        bool TryGetFrame(out FocusFrame frame);
    }
}
