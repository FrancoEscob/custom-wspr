using Microsoft.UI.Xaml;
using System;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.KeyboardAndMouse;

namespace CustomWspr.App.Services;

public class HotkeyService : IDisposable
{
    private const int WM_HOTKEY = 0x0312;
    private const int HOTKEY_ID = 1;

    private HWND _hwnd;
    private Action? _onHotkeyPressed;
    private bool _isRegistered;
    private SubclassProcDelegate? _subclassProc;
    private bool _isSubclassAttached;

    public void Initialize(Window window, Action onHotkeyPressed)
    {
        _onHotkeyPressed = onHotkeyPressed;
        var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
        _hwnd = new HWND(windowHandle);

        _subclassProc = SubclassProc;
        if (SetWindowSubclass(_hwnd, _subclassProc, HOTKEY_ID, 0))
        {
            _isSubclassAttached = true;
        }
    }

    public bool RegisterHotkey(uint vkCode, uint modifiers)
    {
        if (_hwnd.Value == IntPtr.Zero)
        {
            return false;
        }

        try
        {
            _isRegistered = PInvoke.RegisterHotKey(_hwnd, HOTKEY_ID, (HOT_KEY_MODIFIERS)modifiers, vkCode);
            return _isRegistered;
        }
        catch
        {
            return false;
        }
    }

    private nint SubclassProc(HWND hwnd, uint msg, nuint wParam, nint lParam, nuint uIdSubclass, nuint dwRefData)
    {
        if (msg == WM_HOTKEY && (int)wParam == HOTKEY_ID)
        {
            _onHotkeyPressed?.Invoke();
            return 0;
        }

        return DefSubclassProc(hwnd, msg, wParam, lParam);
    }

    public void Dispose()
    {
        if (_isRegistered && _hwnd.Value != IntPtr.Zero)
        {
            PInvoke.UnregisterHotKey(_hwnd, HOTKEY_ID);
            _isRegistered = false;
        }

        if (_isSubclassAttached && _subclassProc is not null && _hwnd.Value != IntPtr.Zero)
        {
            RemoveWindowSubclass(_hwnd, _subclassProc, HOTKEY_ID);
            _isSubclassAttached = false;
        }

        _subclassProc = null;
        _onHotkeyPressed = null;
        _hwnd = default;
    }

    private delegate nint SubclassProcDelegate(HWND hWnd, uint msg, nuint wParam, nint lParam, nuint uIdSubclass, nuint dwRefData);

    [DllImport("comctl32.dll", ExactSpelling = true)]
    private static extern bool SetWindowSubclass(HWND hWnd, SubclassProcDelegate pfnSubclass, nuint uIdSubclass, nuint dwRefData);

    [DllImport("comctl32.dll", ExactSpelling = true)]
    private static extern bool RemoveWindowSubclass(HWND hWnd, SubclassProcDelegate pfnSubclass, nuint uIdSubclass);

    [DllImport("comctl32.dll", ExactSpelling = true)]
    private static extern nint DefSubclassProc(HWND hWnd, uint msg, nuint wParam, nint lParam);
}
