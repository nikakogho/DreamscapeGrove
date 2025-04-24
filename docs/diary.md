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
