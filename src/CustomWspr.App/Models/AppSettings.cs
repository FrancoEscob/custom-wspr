namespace CustomWspr.App.Models;

public class AppSettings
{
    public string UiLanguage { get; set; } = "en-US";
    public HotkeySettings Hotkeys { get; set; } = new();
}

public class HotkeySettings
{
    public string OverlayToggle { get; set; } = "Ctrl+Shift+M";
    public string CommandPalette { get; set; } = "Alt+Space";
}
