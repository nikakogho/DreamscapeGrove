# DreamscapeGrove 🌲🧠

A neurofeedback sandbox where a living forest grows only when the player’s
**focus** (measured eventually from any BCI headset) exceeds configurable thresholds.
The project serves two goals:

1. **Focus-training game** – calm, positive-reinforcement loop for users to help them gain focus and self-control.
2. **Open BCI playground** – pluggable interface so researchers can swap
   in new headsets or signal-processing pipelines with minimal code.

## Support Devices
At first only **Mock** signals (sine wave).  
**Device Pick Dropdown** in the Settings UI lets you switch sources; upcoming adapters will add *Neurosity Crown*, *Muse*, etc.

## User Quick Start

Will deploy and put link here eventually

## Developer Quick Start

### Clone on local

```bash
git clone https://github.com/nikakogho/DreamscapeGrove.git
```

### Open with Unity Hub

May need to install the Unity version first

### In Unity

1. Open Assets/Scenes/SampleScene.Unity
2. Press **Play**
3. Toggle settings UI with the ⚙ icon or by pressing F1
4. Settings
   - Use the **Thresholds Panel** (top right)
      - *Min. Focus* - focus threshold you can adjust for more/less sensitive experience
      - *Min. Confidence* - mock source gives 0.95, choose based on your experience
   - Use device dropdown to pick the device (currently only **Mock**)

- Live focus graph with confidence-based colours  
  * Green = confidence ≥ confidence threshold  
  * Yellow→Red = below confidence threshold  
  * Black bar = required focus level (moves when focus threshold adjusted in settings)

## Dependencies

| Package | Version | Why |
|---------|---------|-----|
| TextMesh Pro | 3.2+ (comes with Unity) | Slider labels & future HUD |
| URP  | 12 LTS    | Lightweight, WebGL-friendly visuals |

These are declared in **Packages/manifest.json**; Unity will install them automatically.

## Folder Structure

```
Assets/
└─ Scripts/
   ├─ Core/          # device-agnostic interfaces + FocusManager
   ├─ Adapters/      # one class per BCI implementation
   ├─ Gameplay/      # tree growth & world behaviours
   └─ UI/            # settings panel, HUD
```

- Core/FocusInterfaces.cs – defines FocusFrame and IFocusSource
- Core/FocusManager.cs – singleton that polls the active source
- Adapters/MockFocusSource.cs – sine-wave mock (for headless tests)
- Gameplay/TreeGrowth.cs – scales a tree based on focus/confidence

## Roadmap

| Milestone | Status       | Target |
| --------  | ----         | ------- |
| 0.1       | ✓           | Settings UI – sliders for thresholds, device dropdown |
| 0.2       | ✓           | on-screen focus graph |
| 0.3       | ☐           | Neurosity “focus” adapter |
| 0.4       | ☐           | Raw EEG processing (theta:beta) + artifact rejection |
| 0.5       | ☐           | WebGL build + first YouTube demo |

## Contributing

- Fork -> branch -> PR
- Keep non-Unity logic in **Core/**
- New headsets: implement **IFocusSource** and drop it in **Adapters/**

## License

MIT - do whatever you want with this, feel free to use it for learning, work, commercially, research, whatever, just don't use it for evil