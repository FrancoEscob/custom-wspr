# Custom WSPR

A local-first speech recognition application for Windows 11 with offline ASR capabilities.

## Project Status: Phase 01 (UI Shell) ✅

WinUI 3 foundation complete with hotkey system, system tray integration, and overlay window.

### Features Implemented

- ✅ WinUI 3 app shell with .NET 8
- ✅ System tray icon with context menu
- ✅ Global hotkey registration (Ctrl+Shift+M)
- ✅ TopMost overlay window with state management
- ✅ MVVM architecture with CommunityToolkit.Mvvm
- ✅ Reusable XAML styles (ResourceDictionaries)
- ✅ Settings persistence (JSON)
- ✅ Logging service

### Quick Start

See [BUILD.md](BUILD.md) for detailed build instructions.

**Requirements**: Windows 11, Visual Studio 2022, .NET 8 SDK

```powershell
# Open solution in Visual Studio 2022
start CustomWspr.sln

# Or build from command line
dotnet build -c Debug
dotnet run --project src/CustomWspr.App/CustomWspr.App.csproj
```

### Architecture

See [docs/architecture.md](docs/architecture.md) for system design and [docs/ux-spec.md](docs/ux-spec.md) for UX specifications.

### Roadmap

- **Phase 01 (Current)**: UI Shell, hotkeys, tray, overlay ✅
- **Phase 02**: Audio capture (WASAPI/NAudio), VAD (Silero ONNX)
- **Phase 03**: ASR engine integration (ONNX Runtime Whisper / Faster-Whisper)
- **Phase 04**: Post-processing, text output modes, mini editor
- **Phase 05**: Model management, settings UI
- **Phase 06**: Polish, packaging (MSIX), telemetry