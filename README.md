# Valo Toolkit

Valo Toolkit is a lightweight WinForms utility for Valorant display and crosshair setup.

## Features

- Switch common 4:3 resolutions quickly.
- Reset to the best native 16:9 resolution available on the current display.
- Toggle monitor devices when needed for special 4:3 workflows.
- Open NVIDIA Control Panel for custom resolution setup.
- Copy preset Valorant crosshair codes.
- Open the VCRDB crosshair website.
- Dark, compact WinForms interface with no web UI or heavy UI framework.

## Included Presets

### Resolutions

- 1568 x 1080
- 1280 x 882
- 1440 x 1080
- 1280 x 960
- 1280 x 1024

### Crosshairs

- 经典十字
- 经典红点
- 空心十字
- 扁平准星

## Build

This project is currently a single-file WinForms app.

Example build command:

```powershell
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /nologo /target:winexe /win32icon:Assets\Icons\app_icon.ico /out:VT.exe /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:System.Management.dll ValoToolkit.cs
```

## Assets

- `Assets/Icons/app_icon.ico`
- `Assets/Icons/app_icon.png`
- `Assets/Crosshairs/*.png`

## Notes

- Windows only.
- Some monitor operations require administrator permission.
- NVIDIA custom resolutions must still be created manually in NVIDIA Control Panel or NVIDIA App.
