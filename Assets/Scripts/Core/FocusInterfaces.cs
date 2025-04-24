namespace DreamscapeGrove.Core
{
    public struct FocusFrame
    {
        public double timestamp;
        public float focus;
        public float confidence;
    }

    public interface IFocusSource
    {
        string Name { get; }
        void Init();
        bool TryGetFrame(out FocusFrame frame);
    }
}
