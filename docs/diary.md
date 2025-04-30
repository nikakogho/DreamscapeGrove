## 2025-04-24

### Commit 1

* **Initial pipeline up:**  
  - Set up folder structure under `Assets/Scripts/` (`Core`, `Adapters`, `Gameplay`).  
  - Implemented `FocusFrame` + `IFocusSource` interface in `Core`.  
  - Added `MockFocusSource` (sine-wave focus) and `FocusManager` singleton.  
  - Created `TreeGrowth` behaviour; cube grows when focus ≥ 0.70 & confidence ≥ 0.90.

* **Visual stability fix:**  
  - Slowed sine wave (`_phase += 0.03f`) to reduce jitter.  
  - Added exponential moving average (α = 0.1) inside `TreeGrowth` for smooth transitions.  
  - Verified in Play mode: tree now breathes steadily between scale 1 ↔ 2 with no flicker.

* **Decisions logged:**  
  - Kept `Init()` synchronous; adapters can start their own async tasks internally.  
  - All classes placed in `DreamscapeGrove.*` namespaces for clarity.  
  - Next task: build Settings UI sliders to edit focus & confidence thresholds at runtime.

Commit: `feat: smooth tree growth + initial diary entry (#dreamscapegrove)`  
Tag: `v0.0.1-alpha`

### Commit 2

* Centralised thresholds in `FocusManager` (`FocusThreshold`, `ConfidenceThreshold`)
  and removed per-tree sliders.
* Implemented `FocusSettingsUI` – sliders now call
  `FocusManager.SetFocusThreshold/SetConfidenceThreshold`.
* UI polish: renamed labels to “Min. Focus” / “Min. Confidence” and
  added live numeric display.
* Verified: tree growth responds instantly to new slider settings.

Commit: `global thresholds + settings UI (#dreamscapegrove)`  
Tag: `v0.0.2-alpha`

## 2025-04-25

* Added **device dropdown** to SettingsPanel.
* `FocusManager` now exposes `FocusDevice` enum, factory, and `SwitchDevice()`.
* UI auto-populates from `FocusManager.AvailableDevices`; currently only “Mock”.
* Prepped for future adapters (IDisposable clean-up hook).
* Wrapped all UI elements under `SettingsRoot`.

* Added `SettingsToggleUI`:
  - **F1** toggles visibility.
  - Gear button in top-right calls same method.
* Scene stays photo-real when UI is hidden; quick access during testing.

Commit: feat(ui): add device dropdown + F1/gear toggle for Settings panel (#dreamscapegrove)
Tag   : v0.0.3-alpha

## 2025-04-29

### focus graph & threshold

* Implemented `UILineGraph` — Canvas-native polyline with 1-px quads.
* `CurrentFocusUI` feeds frames at 20 Hz; buffer length equals rect width so graph always scrolls.
* Colour logic: green above confidence threshold, red→yellow below.
* Added cyan horizontal “Required Focus” line + label that tracks slider.

Commit: focus graph colours + moving threshold bar (#dreamscapegrove)
Tag   : v0.0.3-alpha

## 2025-04-30

* Added **focus-osc-bridge.js** – logs into Crown via Neurosity SDK and rebroadcasts `/focus` on UDP 9001 (localhost).
* New **NodeBridgeProcess** auto-spawns the bridge at game launch and kills it on exit.
* Refactored OSC adapter → `NeurosityWsFocusSource` (default port 9001).
* Updated README with environment variables and new setup steps.
* Live focus now controls DreamscapeGrove with ~300 ms latency.

Commit: Crown SDK focus bridge + auto launcher (#dreamscapegrove)
Tag   : v0.1.0
