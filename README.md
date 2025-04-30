# DreamscapeGrove 🌲🧠

A neurofeedback sandbox where a living forest grows only when the player’s **focus** exceeds configurable thresholds.
The project serves two goals:

1. **Focus-training game** – calm, positive-reinforcement loop for users to help them gain focus and self-control.
2. **Open BCI playground** – pluggable interface so researchers can swap in new headsets or signal-processing pipelines with minimal code.

## Supported Devices

| Device | Transport | Metric | Status |
|--------|-----------|--------|--------|
| **Mock generator** | Internal | focus (sine wave) | ✔ |
| **Neurosity Crown** | SDK → OSC bridge | focus (0–1) | ✔ |
| Muse, OpenBCI, etc. | – | – | ☐ (coming) |

A **Device** dropdown in the Settings panel lets you switch sources at runtime.

## User Quick Start

> A WebGL build will be published soon – for now run locally

## Local Quick Start

### Clone on local

```bash
git clone https://github.com/nikakogho/DreamscapeGrove.git
```

### Set environment variables

| Name         | Example               | Purpose                                                        |
| --------     | ----                  | -------                                                        |
| CROWN_ID     | Crown-A08             | Device ID from console.neurosity.co                            |
| CROWN_EMAIL  | you@example.com       | Login for SDK bridge                                           |
| CROWN_PWD    | yourPassword          | Password for SDK bridge                                        |
| FOCUS_PORT   | 9001 (default value)  | Port on localhost to stream focus metrics to, change if needed |

### Open with Unity Hub

Make sure you have Unity 2021 installed

### In Unity

1. Open Assets/Scenes/SampleScene.Unity
2. Press **Play**
   - The game auto-launches `focus-osc-bridge.js`
   - logs into the Crown via the Neurosity SDK
   - rebroadcasts `/focus` on UDP 9001
3. Concentrate - focus bar goes up and once it crosses the threshold, tree starts to grow
4. Toggle settings UI with the ⚙ icon or by pressing F1
5. Settings
   - Use the **Thresholds Panel** (top right)
      - *Min. Focus* - focus threshold you can adjust for more/less sensitive experience
      - *Min. Confidence* - mock source gives 0.95, choose based on your experience
   - Use device dropdown to pick the device (currently only **Mock**)

- Live focus graph with confidence-based colours  
  * Green = confidence ≥ confidence threshold  
  * Yellow→Red = below confidence threshold  
  * Horizontal Black bar = required focus level (moves when focus threshold adjusted in settings)

## Dependencies

| Package | Version | Why |
|---------|---------|-----|
| TextMesh Pro | 3.2+ (comes with Unity) | Slider labels & future HUD |
| URP  | 12 LTS    | Lightweight, WebGL-friendly visuals |
| @neurosity/sdk  | npm   | Crown focus stream |
| osc / dotenv  | npm    | Bridge & env vars |

Unity packages auto-install via Packages/manifest.json; Node packages install with npm install

## Folder Structure

```
Assets/
└─ Scripts/
   ├─ Core/          # device-agnostic interfaces + FocusManager
   ├─ Adapters/      # one class per BCI implementation
   ├─ Gameplay/      # tree growth & world behaviours
   └─ UI/            # settings panel, HUD
```

### Unity Side

- Core/FocusInterfaces.cs – defines FocusFrame and IFocusSource
- Core/FocusManager.cs – singleton that polls the active source
- Adapters/MockFocusSource.cs – sine-wave mock (for headless tests)
- Adapters/NeurosityWsFocusSource.cs - reads focus from localhost:9001 UDP
- Gameplay/TreeGrowth.cs – scales a tree based on focus/confidence

### Node.js side

- focus-osc-bridge.js - streams focus metric from Neurosity's server and relays it on localhost:9001 UDP

## Roadmap

| Milestone | Status       | Target |
| --------  | ----         | ------- |
| 0.1       | ✓           | Settings UI – sliders for thresholds, device dropdown |
| 0.2       | ✓           | on-screen focus graph |
| 0.3       | ✓           | Neurosity “focus” adapter |
| 0.4       | ☐           | Raw EEG processing (theta:beta) + artifact rejection |
| 0.5       | ☐           | WebGL build + first YouTube demo |

## Contributing

- Fork -> branch -> PR
- Keep non-Unity logic in **Core/**
- New headsets: implement **IFocusSource** and drop it in **Adapters/**

## License

MIT - do whatever you want with this, feel free to use it for learning, work, commercially, research, whatever, just don't use it for evil