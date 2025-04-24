using DreamscapeGrove.Core;
using System;

namespace DreamscapeGrove.Adapters
{
    public class MockFocusSource : IFocusSource
    {
        private float _phase;
        private float _phasingSpeed;

        public string Name => "Mock";

        public void Init()
        {
            _phase = 0;
            _phasingSpeed = 0.01f;
        }

        public bool TryGetFrame(out FocusFrame frame)
        {
            _phase += _phasingSpeed;

            frame = new FocusFrame
            {
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                focus = 0.5f + 0.4f * MathF.Sin(_phase),
                confidence = 0.95f
            };

            return true;
        }
    }
}
