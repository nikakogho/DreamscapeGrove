using UnityEngine;
using DreamscapeGrove.Core;

namespace DreamscapeGrove.Gameplay
{
    public class TreeGrowth : MonoBehaviour
    {
        [SerializeField] private float growthRatePerS = 0.3f;
        [SerializeField] private float shrinkRatePerS = 0.15f;

        private FocusManager _focusManager;
        private float _smoothedFocus;

        private void Start()
        {
            _focusManager = FocusManager.Instance;
            _smoothedFocus = _focusManager.CurrentFrame.focus;
        }

        private void Update()
        {
            var frame = _focusManager.CurrentFrame;

            _smoothedFocus = Mathf.Lerp(_smoothedFocus, frame.focus, 0.1f);
            var shouldGrow = _smoothedFocus >= FocusManager.FocusThreshold && frame.confidence >= FocusManager.ConfidenceThreshold;

            float targetY = shouldGrow ? 2 : 1;
            float rate = shouldGrow ? growthRatePerS : shrinkRatePerS;

            var targetScale = new Vector3(1, targetY, 1);

            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, rate * Time.deltaTime);
        }
    }
}
