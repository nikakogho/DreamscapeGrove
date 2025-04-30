using System;
using System.Net;
using System.Net.Sockets;
using DreamscapeGrove.Core;
using UnityEngine;

namespace DreamscapeGrove.Adapters
{
    public class NeurosityWsFocusSource : IFocusSource, IDisposable
    {
        private const int PORT = 9001;
        private const float CONFIDENCE = 0.95f; // crown doesn't give it

        private UdpClient _udp;
        private IPEndPoint ep;
        private float focus;

        public string Name => "Neurosity Crown (SDK)";

        public void Dispose()
        {
            _udp.Close();
            Debug.Log("Closed UDP");
        }

        public void Init()
        {
            _udp = new(PORT);
            ep = new(IPAddress.Loopback, 0);
        }

        public bool TryGetFrame(out FocusFrame frame)
        {
            while (_udp.Available > 0)
            {
                byte[] buf = _udp.Receive(ref ep);

                Debug.Log("Step 0");
                if (buf.Length < 16 || buf[0] != '/' || buf[1] != 'f') continue;

                Debug.Log("Step 1");
                // Find start of type-tag string (next 4-byte boundary after address)
                int addrEnd = Array.IndexOf(buf, (byte)0);          // first null
                int typeTagOffset = (addrEnd + 4) & ~3;            // align 4
                if (buf[typeTagOffset] != ',' || buf[typeTagOffset + 1] != 'f')
                    continue;                                      // not a float msg

                Debug.Log("Step 2");
                int valueOffset = typeTagOffset + 4;               // ",f\0" padded

                // OSC is big-endian; BitConverter expects little on Windows
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(buf, valueOffset, 4);
                }
                focus = BitConverter.ToSingle(buf, valueOffset);
                Debug.Log("Focus = " + focus);
            }

            frame = new()
            {
                timestamp = Time.realtimeSinceStartupAsDouble,
                focus = focus,
                confidence = CONFIDENCE
            };

            return true;
        }
    }
}
