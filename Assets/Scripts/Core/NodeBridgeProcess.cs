using System.Diagnostics;
using UnityEngine;

namespace DreamscapeGrove.Core
{
    public class NodeBridgeProcess : MonoBehaviour
    {
        [SerializeField] private string scriptName = "focus-osc-bridge.js";
        private Process _proc;

        private void Start()
        {
            string node = "node";
            string script = $"{Application.dataPath}/../{scriptName}";
            var psi = new ProcessStartInfo(node, script)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            _proc = Process.Start(psi);
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            if (_proc != null && !_proc.HasExited) _proc.Kill();
        }
    }
}
