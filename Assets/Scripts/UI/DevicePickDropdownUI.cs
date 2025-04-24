using DreamscapeGrove.Core;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DreamscapeGrove.UI
{
    public class DevicePickDropdownUI : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;

        void Start()
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(
                FocusManager.AvailableDevices
                           .Select(d => d.ToString())
                           .ToList());

            dropdown.onValueChanged.AddListener(OnDeviceChanged);
        }

        private void OnDeviceChanged(int index)
        {
            var selected = FocusManager.AvailableDevices[index];
            FocusManager.SwitchDevice(selected);
        }
    }
}
