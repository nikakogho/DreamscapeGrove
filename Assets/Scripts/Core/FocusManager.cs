using UnityEngine;
using DreamscapeGrove.Adapters;
using System.Collections.Generic;

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

        public static IReadOnlyList<FocusDevice> AvailableDevices = new []
        {
            FocusDevice.Mock,
            FocusDevice.Neurosity
        };

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Source = new MockFocusSource();
            Source.Init();
        }

        private void Update()
        {
            if (Source.TryGetFrame(out var frame)) CurrentFrame = frame;
        }

        public static void SwitchDevice(FocusDevice device)
        {
            Instance.Source?.DisposeIfNeeded();
            Instance.Source = CreateAdapter(device);
            Instance.Source.Init();
        }

        private static IFocusSource CreateAdapter(FocusDevice device) =>
            device switch
            {
                FocusDevice.Mock => new MockFocusSource(),
                FocusDevice.Neurosity => new NeurosityWsFocusSource(),
                _ => new MockFocusSource()
            };

        public static void SetFocusThreshold(float focus)
        {
            FocusThreshold = Mathf.Clamp01(focus);
        }

        public static void SetConfidenceThreshold(float confidence)
        {
            ConfidenceThreshold = Mathf.Clamp01(confidence);
        }

        private void OnApplicationQuit()
        {
            Instance.Source?.DisposeIfNeeded();
        }
    }
}
