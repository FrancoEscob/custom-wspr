using CustomWspr.App.Services;
using CustomWspr.App.UI.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace CustomWspr.App;

public partial class App : Application
{
    private IServiceProvider? _serviceProvider;
    private NotifyIconService? _notifyIconService;
    private HotkeyService? _hotkeyService;
    private LoggingService? _loggingService;
    private const uint ModControl = 0x0002;
    private const uint ModShift = 0x0004;
    private const uint VkM = 0x0000004D;

    public App()
    {
        InitializeComponent();
        ConfigureServices();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<SettingsService>();
        services.AddSingleton<LoggingService>();
        services.AddSingleton<NotifyIconService>();
        services.AddSingleton<HotkeyService>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<OverlayWindow>();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        if (_serviceProvider is null)
        {
            return;
        }

        _loggingService = _serviceProvider.GetRequiredService<LoggingService>();
        _notifyIconService = _serviceProvider.GetRequiredService<NotifyIconService>();
        _hotkeyService = _serviceProvider.GetRequiredService<HotkeyService>();

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Activate();

        _notifyIconService.Initialize(mainWindow, ShowMainWindow, ExitApp);
        _hotkeyService.Initialize(mainWindow, ToggleOverlay);

        var modifiers = ModControl | ModShift;
        if (!_hotkeyService.RegisterHotkey(VkM, modifiers))
        {
            _loggingService?.Log("Failed to register overlay toggle hotkey.");
        }
        else
        {
            _loggingService?.Log("Overlay hotkey registered (Ctrl+Shift+M).");
        }

        _loggingService?.Log("Application launched");
    }

    private void ShowMainWindow()
    {
        if (_serviceProvider == null) return;

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Activate();
        _loggingService?.Log("Main window opened");
    }

    private void ToggleOverlay()
    {
        if (_serviceProvider == null) return;

        var overlay = _serviceProvider.GetRequiredService<OverlayWindow>();
        overlay.Toggle();
        _loggingService?.Log($"Overlay toggled: {overlay.IsVisible}");
    }

    private void ExitApp()
    {
        _loggingService?.Log("Application exiting");
        _hotkeyService?.Dispose();
        _notifyIconService?.Dispose();
        Application.Current.Exit();
    }
}
