# Valo Toolkit

Valo Toolkit is a lightweight Windows WinForms utility for Valorant display and crosshair setup.

## Features

- Switch common 4:3 resolutions.
- Reset to the best native 16:9 resolution available on the current display.
- Toggle monitor devices for special 4:3 workflows.
- Open NVIDIA Control Panel for custom resolution setup.
- Copy built-in Valorant crosshair codes.
- Open the VCRDB crosshair website.
- One-click Valorant optimization actions:
  - Set currently running `SGuard64.exe` and `SGuardSvc64.exe` to below-normal priority and the last logical CPU.
  - Disable Windows memory integrity through an explicit administrator-confirmed action.
  - Disable fullscreen optimizations for `aclos-launcher.exe` in a selected ACLOS folder.
  - Delete the user's `AppData\LocalLow\NVIDIA\DXCache` folder after confirmation.
- Dark, compact UI built with WinForms.

## Included Resolution Presets

- 1568 x 1080
- 1280 x 882
- 1440 x 1080
- 1280 x 960
- 1280 x 1024

## Included Crosshair Presets

- Classic Cross
- Classic Red Dot
- Hollow Cross
- Flat Crosshair

## Build

This project is currently a single-file WinForms app.

Example build command:

```powershell
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /nologo /target:winexe /win32icon:Assets\Icons\app_icon.ico /out:VT.exe /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:System.Management.dll /resource:Assets\Crosshairs\classic_cross.png,ValorantResolutionAssistant.Assets.Crosshairs.classic_cross.png /resource:Assets\Crosshairs\classic_red_dot.png,ValorantResolutionAssistant.Assets.Crosshairs.classic_red_dot.png /resource:Assets\Crosshairs\flat_crosshair.png,ValorantResolutionAssistant.Assets.Crosshairs.flat_crosshair.png /resource:Assets\Crosshairs\hollow_cross.png,ValorantResolutionAssistant.Assets.Crosshairs.hollow_cross.png ValoToolkit.cs
```

## Assets

- `Assets/Icons/app_icon.ico`
- `Assets/Icons/app_icon.png`
- `Assets/Crosshairs/*.png`

## Notes

- Windows only.
- Some monitor operations require administrator permission.
- ACE process settings apply to the currently running processes only and must be reapplied after ACE restarts.
- Disabling memory integrity reduces Windows kernel protection and may require a reboot; use the action only when you understand the security tradeoff.
- ACLOS optimization is stored per user through Windows compatibility settings and automatically searches fixed drives for `WeGameApps\rail_apps\*\ACLOS\aclos-launcher.exe`.
- Crosshair previews are embedded in `VT.exe`, so they remain available after moving the executable without its `Assets` folder.
- NVIDIA DXCache is a rebuildable cache, but the toolkit still asks for confirmation before deleting it.
- NVIDIA custom resolutions must still be created manually in NVIDIA Control Panel or NVIDIA App.
- No WPF, Electron, Avalonia, MAUI, WebView, or HTML UI is used.

## Backlog

- Investigate NVIDIA Control Panel compatibility failures on other computers and add a safe fallback for opening it.
