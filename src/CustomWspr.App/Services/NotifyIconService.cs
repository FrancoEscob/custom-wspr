using Microsoft.UI.Xaml;

namespace CustomWspr.App.Services;

public class NotifyIconService : IDisposable
{
    private bool _isInitialized;
    private Action? _onOpenSettings;
    private Action? _onExit;

    public void Initialize(Window window, Action onOpenSettings, Action onExit)
    {
        _onOpenSettings = onOpenSettings;
        _onExit = onExit;
        
        // TODO: Implement system tray icon using Shell_NotifyIcon
        // For Phase 01, this is a placeholder. Tray icon functionality will be implemented later.
        
        _isInitialized = true;
    }

    public void Dispose()
    {
        if (_isInitialized)
        {
            // TODO: Remove tray icon
            _isInitialized = false;
        }
    }
}
