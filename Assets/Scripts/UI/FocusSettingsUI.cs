using DreamscapeGrove.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DreamscapeGrove.UI
{
    public class FocusSettingsUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Slider focusSlider;
        [SerializeField] private Slider confidenceSlider;
        [SerializeField] private TMP_Text focusLabel;
        [SerializeField] private TMP_Text confidenceLabel;

        private void Start()
        {
            focusSlider.value = FocusManager.FocusThreshold;
            confidenceSlider.value = FocusManager.ConfidenceThreshold;
            UpdateLabels();
        }

        public void OnFocusSliderChanged()
        {
            FocusManager.SetFocusThreshold(focusSlider.value);
            UpdateLabels();
        }

        public void OnConfidenceSliderChanged()
        {
            FocusManager.SetConfidenceThreshold(confidenceSlider.value);
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            focusLabel.text = $"Min. Focus: {focusSlider.value:0.00}";
            confidenceLabel.text = $"Min. Confidence: {confidenceSlider.value:0.00}";
        }
    }
}
