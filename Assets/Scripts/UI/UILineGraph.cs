using DreamscapeGrove.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DreamscapeGrove.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class UILineGraph : Graphic
    {
        [SerializeField] private float strokeWidth = 2f;

        private CircularBuffer<FocusFrame> data;

        public int PointsAmount => data.Capacity;

        protected override void Awake()
        {
            var width = (transform as RectTransform).rect.width;

            data = new CircularBuffer<FocusFrame>((int)width);
        }

        /// <summary>Push a new sample. Call once per frame.</summary>
        public void AddFocusFrame(FocusFrame frame)
        {
            if (data == null) return;

            var clampedFrame = new FocusFrame
            {
                focus = Mathf.Clamp01(frame.focus),
                confidence = Mathf.Clamp01(frame.confidence),
                timestamp = frame.timestamp
            };

            data.Push(clampedFrame);
            SetVerticesDirty(); // triggers OnPopulateMesh
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            if (data == null) return;

            vh.Clear();
            if (data.Count < 2) return;

            float w = rectTransform.rect.width;
            float h = rectTransform.rect.height;
            int n = data.Count;
            float dx = w / (n - 1);

            float confThresh = FocusManager.ConfidenceThreshold;
            var lastColor = ColorForConfidence(data[0].confidence, confThresh);

            Vector2 prev = new Vector2(0, data[0].focus * h);

            for (int i = 1; i < n; i++)
            {
                var curr = new Vector2(i * dx, data[i].focus * h);
                var currentColor = ColorForConfidence(data[i].confidence, confThresh);

                DrawQuad(vh, prev, curr, strokeWidth, lastColor, currentColor);
                
                prev = curr;
                lastColor = currentColor;
            }
        }

        private static Color ColorForConfidence(float color, float threshold)
        {
            if (color >= threshold) return Color.green;
            float t = Mathf.InverseLerp(0f, threshold, color);

            return Color.Lerp(Color.red, Color.yellow, t);
        }

        private static void DrawQuad(VertexHelper vh, Vector2 a, Vector2 b, float width, Color lastColor, Color currentColor)
        {
            Vector2 dir = (b - a).normalized;
            Vector2 normal = 0.5f * width * new Vector2(-dir.y, dir.x);

            int index = vh.currentVertCount;

            vh.AddVert(a - normal, lastColor, Vector2.zero);
            vh.AddVert(a + normal, lastColor, Vector2.zero);
            vh.AddVert(b + normal, currentColor, Vector2.zero);
            vh.AddVert(b - normal, currentColor, Vector2.zero);

            vh.AddTriangle(index, index + 1, index + 2);
            vh.AddTriangle(index, index + 2, index + 3);
        }

        // minimal ring-buffer
        private class CircularBuffer<T>
        {
            private readonly T[] buf;
            private int head;
            public int Count { get; private set; }
            public int Capacity => buf.Length;
            public CircularBuffer(int capacity) { buf = new T[capacity]; }
            public void Push(T item)
            {
                buf[head] = item;
                head = (head + 1) % buf.Length;
                if (Count < buf.Length) Count++;
            }
            public T this[int i] => buf[(head - Count + i + buf.Length) % buf.Length];
        }
    }
}
