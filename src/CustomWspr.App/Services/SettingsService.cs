using CustomWspr.App.Models;
using System.Text.Json;

namespace CustomWspr.App.Services;

public class SettingsService
{
    private readonly string _settingsFilePath;
    private AppSettings _settings;

    public SettingsService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var settingsDir = Path.Combine(appDataPath, "CustomWspr");
        Directory.CreateDirectory(settingsDir);
        _settingsFilePath = Path.Combine(settingsDir, "settings.json");
        
        _settings = LoadSettings();
    }

    public AppSettings Settings => _settings;

    private AppSettings LoadSettings()
    {
        try
        {
            if (File.Exists(_settingsFilePath))
            {
                var json = File.ReadAllText(_settingsFilePath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
        }
        catch
        {
        }
        
        return new AppSettings();
    }

    public void SaveSettings()
    {
        try
        {
            var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
            File.WriteAllText(_settingsFilePath, json);
        }
        catch
        {
        }
    }

    public void UpdateLanguage(string language)
    {
        _settings.UiLanguage = language;
        SaveSettings();
    }
}
