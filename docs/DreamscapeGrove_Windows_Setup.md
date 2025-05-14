# DreamscapeGrove — Windows Stand‑alone Setup Guide (v0.1.2)

Follow these steps to run DreamscapeGrove without Unity on Windows 10/11.

---

## 1  Prerequisites

| Item | Minimum Version | Notes |
|------|-----------------|-------|
| **Windows** | 10 (1903) or newer | 64‑bit |
| **Graphics** | DX11‑capable GPU | Integrated GPUs work |
| **Node.js** | 18 LTS | Needed for the background **osc‑bridge.js** |
| **Neurosity Crown firmware** | r23+ | For focus metric via SDK |

---

## 2  Install Node & set environment variables

1. Download Node.js LTS from https://nodejs.org and install it.  
2. Open **Start → Environment Variables** → *User variables* → **New…**

   | Name | Value |
   |------|-------|
   | `CROWN_ID` | e.g. `Crown-A08` |
   | `CROWN_EMAIL` | your Neurosity e‑mail |
   | `CROWN_PWD` | account password |

*(Alternatively create a `.env` file next to `DreamscapeGrove.exe` with the same
`KEY=value` lines.)*

Sign out/in so the variables propagate.

---

## 3  Extract & run

1. Unzip **DreamscapeGrove_windows.zip** anywhere (avoid `Program Files` which can block writes).  
2. Double‑click **DreamscapeGrove.exe**.  
3. Windows SmartScreen may warn; click **More info → Run anyway** (unsigned build).  
4. The game launches, auto‑starting a hidden Node process for the Crown focus bridge.

---

## 4  In‑game controls

| Action | Keys / Mouse |
|--------|--------------|
| Toggle Settings panel | **F1** or **⚙** button |
| Move camera | Hold **RMB** + WASD / QE |
| Dolly | Mouse wheel |
| Pan | Hold **MMB** + drag |

Select **Device → Neurosity Crown (SDK)** in the Settings panel and watch the forest respond to your focus.

---

## 5  Troubleshooting

| Symptom | Fix |
|---------|-----|
| Focus graph flat / trees don’t grow | Check that `osc-bridge.js` appears in Task Manager → Details. Verify environment variables. |
| Console shows “login failed” | Wrong `CROWN_*` credentials. |
| Game closes instantly | Update GPU drivers; ensure DX11 support. |

Logs: `%USERPROFILE%\AppData\LocalLow\<CompanyName>\DreamscapeGrove\Player.log`

---

## 6  Uninstall

Delete the unzipped folder; no registry keys or user data are left behind.
