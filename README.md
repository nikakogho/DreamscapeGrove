# DreamscapeGrove 🌲🧠

A neurofeedback sandbox where a living forest grows only when the player’s
**focus** (measured eventually from any BCI headset) exceeds configurable thresholds.
The project serves two goals:

1. **Focus-training game** – calm, positive-reinforcement loop for users to help them gain focus and self-control.
2. **Open BCI playground** – pluggable interface so researchers can swap
   in new headsets or signal-processing pipelines with minimal code.

## Support Devices

At first only mock signals. Next up will be Neurosity Crown, then Muse, then others.

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
2. Press **Play** and enjoy

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

| Milestone    | Target |
| --------     | ------- |
| 0.1          | Settings UI – sliders for thresholds, device dropdown |
| 0.2          | Neurosity “focus” adapter + on-screen focus graph |
| 0.3          | Raw EEG processing (theta:beta) + artifact rejection |
| 0.4          | WebGL build + first YouTube demo |

## Contributing

- Fork -> branch -> PR
- Keep non-Unity logic in Core/
- New headsets: implement IFocusSource and drop it in Adapters/

## License

MIT - do whatever you want with this, feel free to use it for learning, work, commercially, research, whatever, just don't use it for evil