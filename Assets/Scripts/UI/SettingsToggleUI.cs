using UnityEngine;
using UnityEngine.UI;

namespace DreamscapeGrove.UI
{
    public class SettingsToggleUI : MonoBehaviour
    {
        [SerializeField] private GameObject settingsRoot;
        [SerializeField] private Button toggleButton;

        private static KeyCode ToggleKey = KeyCode.F1;

        private void Start()
        {
            toggleButton.onClick.AddListener(Toggle);
        }

        private void Update()
        {
            if (Input.GetKeyDown(ToggleKey)) Toggle();
        }

        private void Toggle()
        {
            settingsRoot.SetActive(!settingsRoot.activeSelf);
        }
    }
}
