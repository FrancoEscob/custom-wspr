# Build Instructions - Custom WSPR Phase 01

## Prerequisites

- Windows 11 (version 21H2 or higher)
- Visual Studio 2022 (17.8 or higher) with:
  - .NET desktop development workload
  - Universal Windows Platform development workload
  - Windows App SDK (included with Visual Studio)
- .NET 8 SDK

## Building the Project

### Option 1: Visual Studio

1. Open `CustomWspr.sln` in Visual Studio 2022
2. Restore NuGet packages (automatic on first build)
3. Select `Debug | x64` configuration
4. Press F5 to build and run

### Option 2: Command Line

```powershell
# Navigate to project directory
cd C:\Users\frand\Desktop\DEV\custom-wspr

# Restore NuGet packages
dotnet restore

# Build the project
dotnet build -c Debug

# Run the project
dotnet run --project src/CustomWspr.App/CustomWspr.App.csproj
```

## Expected Behavior (Phase 01)

✅ **System Tray Icon**: App starts with an icon in the system tray
- Right-click: Context menu with "Open Settings" and "Exit"
- Double-click: Opens settings window

✅ **Global Hotkey**: Press `Ctrl+Shift+M` to toggle overlay
- Opens/closes the overlay window from any application

✅ **Overlay Window**: 
- TopMost window (always on top)
- Displays current state (Idle/Recording)
- Language selector (EN/ES - UI only, no functionality yet)
- Waveform placeholder
- Record button toggles Idle ↔ Recording (dummy state only)
- No actual audio capture in Phase 01

✅ **Settings Window**:
- Tabbed interface (General, Audio, Models, About)
- Shows configured hotkeys
- Language preference selector (persists to JSON)

✅ **Logging**: 
- Events logged to: `%LOCALAPPDATA%\CustomWspr\Logs\app_YYYYMMDD.log`
- Also visible in Debug output window

## Troubleshooting

### "RegisterHotKey failed"
- Another application is using Ctrl+Shift+M
- Close conflicting apps or wait for Phase 02 hotkey configuration

### "Shell_NotifyIcon failed"
- Restart Windows Explorer: `taskkill /f /im explorer.exe && start explorer.exe`

### Missing NuGet packages
```powershell
dotnet restore --force
```

### CsWin32 errors
- Clean and rebuild: `dotnet clean && dotnet build`
- Ensure `NativeMethods.txt` exists in project root

## Project Structure

```
src/CustomWspr.App/
├── App.xaml / App.xaml.cs          # Application entry point, DI setup
├── CustomWspr.App.csproj           # Project file
├── app.manifest                    # High DPI awareness
├── NativeMethods.txt               # CsWin32 P/Invoke declarations
├── Models/
│   ├── AppSettings.cs              # Settings model
│   └── OverlayState.cs             # UI state enum
├── Services/
│   ├── HotkeyService.cs            # Global hotkey registration
│   ├── LoggingService.cs           # File/console logging
│   ├── NotifyIconService.cs        # System tray icon
│   └── SettingsService.cs          # JSON persistence
└── UI/
    ├── Resources/
    │   ├── Buttons.xaml            # Button styles
    │   ├── Colors.xaml             # Color brushes
    │   └── Typography.xaml         # Text styles
    ├── ViewModels/
    │   └── OverlayViewModel.cs     # MVVM for overlay
    └── Windows/
        ├── MainWindow.xaml/.cs     # Settings window
        └── OverlayWindow.xaml/.cs  # TopMost overlay

Settings file: %LOCALAPPDATA%\CustomWspr\settings.json
Log files: %LOCALAPPDATA%\CustomWspr\Logs\
```

## Next Steps (Phase 02)

- Audio capture via WASAPI (NAudio)
- VAD integration (Silero ONNX)
- Real-time waveform visualization
- Segmentation logic

## Known Limitations (Phase 01)

- ❌ No actual audio recording
- ❌ Recording states are UI-only (dummy)
- ❌ Hotkey cannot be reconfigured yet
- ❌ Language selector doesn't change UI language
- ❌ Waveform is a placeholder
- ❌ Timer doesn't increment during recording
