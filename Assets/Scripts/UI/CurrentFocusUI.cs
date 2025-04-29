using DreamscapeGrove.Core;
using TMPro;
using UnityEngine;

namespace DreamscapeGrove.UI
{
    [RequireComponent(typeof(UILineGraph))]
    public class CurrentFocusUI : MonoBehaviour
    {
        [SerializeField] private float _sampleInterval = 0.05f; // 20 Hz
        [SerializeField] private RectTransform _focusThresholdLine;
        [SerializeField] private TMP_Text _focusText;

        private RectTransform _rect;

        private UILineGraph _graph;
        private float _timer;

        private void Awake()
        {
            _graph = GetComponent<UILineGraph>();
            _rect = transform as RectTransform;
        }

        private void Start()
        {
            for (int i = 0; i < _graph.PointsAmount; i++)
            {
                _graph.AddFocusFrame(FocusManager.Instance.CurrentFrame);
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _sampleInterval)
            {
                _timer = 0f;

                var frame = FocusManager.Instance.CurrentFrame;

                _graph.AddFocusFrame(frame);
                _focusText.text = $"Focus: {frame.focus:0.00}";
            }
        }

        private void LateUpdate()
        {
            float h = _rect.rect.height;
            float y = FocusManager.FocusThreshold * h;

            _focusThresholdLine.anchoredPosition = new Vector2(0f, y);
        }
    }
}
