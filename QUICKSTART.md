# Quick Start Guide

## Build & Run

```powershell
cd C:\Users\frand\Desktop\DEV\custom-wspr
dotnet restore
dotnet build -c Debug
dotnet run --project src/CustomWspr.App/CustomWspr.App.csproj
```

Or open `CustomWspr.sln` in Visual Studio 2022 and press F5.

## What to Expect

1. **System Tray**: Icon appears in notification area
   - Right-click: Open Settings / Exit
   - Double-click: Open Settings

2. **Global Hotkey**: `Ctrl+Shift+M`
   - Opens/closes overlay window from anywhere

3. **Overlay Window**:
   - Always on top
   - Click "Record (R)": Toggles Idle ↔ Recording (UI only, no audio yet)
   - "Esc" button: Closes overlay

4. **Settings Window**:
   - General: Language, Hotkeys
   - Audio/Models: Placeholder (Phase 02+)
   - About: Version info

## Files to Check

- Logs: `%LOCALAPPDATA%\CustomWspr\Logs\app_YYYYMMDD.log`
- Settings: `%LOCALAPPDATA%\CustomWspr\settings.json`

## Troubleshooting

- **Hotkey conflict**: Close other apps using Ctrl+Shift+M
- **Build errors**: Run `dotnet clean && dotnet build`
- **Tray icon missing**: Restart Explorer

## Phase 01 Limitations

- ❌ No audio capture (Recording is UI-only)
- ❌ Timer doesn't increment
- ❌ Waveform is static placeholder
- ❌ Hotkey can't be reconfigured yet

These are intentional - Phase 02 adds audio + VAD!

## Next Steps

Read [PHASE-01-SUMMARY.md](PHASE-01-SUMMARY.md) for full details and testing checklist.
