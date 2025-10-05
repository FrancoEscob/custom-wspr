using CustomWspr.App.UI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using WinRT.Interop;

namespace CustomWspr.App.UI.Windows;

public sealed partial class OverlayWindow : Window
{
    public OverlayViewModel ViewModel { get; }
    public bool IsVisible { get; private set; }
    private AppWindow? _appWindow;
    private HWND _hwnd;

    public OverlayWindow()
    {
        ViewModel = new OverlayViewModel();
        InitializeComponent();

        // Note: WinUI 3 Windows don't have DataContext, binding is done via x:Bind to ViewModel property
        SetupWindowStyle();
        IsVisible = false;
    }

    private void SetupWindowStyle()
    {
        var hwnd = WindowNative.GetWindowHandle(this);
        _hwnd = new HWND(hwnd);

        var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
        _appWindow = AppWindow.GetFromWindowId(windowId);

        if (_appWindow != null)
        {
            // Set window size
            _appWindow.Resize(new global::Windows.Graphics.SizeInt32(480, 320));
            
            var presenter = _appWindow.Presenter as OverlappedPresenter;
            if (presenter != null)
            {
                presenter.IsAlwaysOnTop = true;
                presenter.IsResizable = false;
                presenter.IsMaximizable = false;
                presenter.IsMinimizable = false;
            }
        }

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(null);
    }

    public void Toggle()
    {
        if (IsVisible)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        if (_appWindow != null)
        {
            _appWindow.Show();
        }
        else if (_hwnd.Value != IntPtr.Zero)
        {
            ShowWindowNative(_hwnd, SW_SHOW);
        }

        Activate();
        RootBorder?.Focus(FocusState.Programmatic);
        IsVisible = true;
        CenterOnScreen();
    }

    public void Hide()
    {
        if (_appWindow != null)
        {
            _appWindow.Hide();
        }
        else if (_hwnd.Value != IntPtr.Zero)
        {
            ShowWindowNative(_hwnd, SW_HIDE);
        }

        IsVisible = false;
    }

    private void CenterOnScreen()
    {
        if (_appWindow != null)
        {
            var displayArea = DisplayArea.GetFromWindowId(_appWindow.Id, DisplayAreaFallback.Primary);
            var workArea = displayArea.WorkArea;

            var x = (workArea.Width - _appWindow.Size.Width) / 2;
            var y = (workArea.Height - _appWindow.Size.Height) / 2;

            _appWindow.Move(new global::Windows.Graphics.PointInt32(x, y));
            return;
        }

        if (_hwnd.Value != IntPtr.Zero)
        {
            var windowId = Win32Interop.GetWindowIdFromWindow(_hwnd.Value);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            if (appWindow != null)
            {
                var displayArea = DisplayArea.GetFromWindowId(appWindow.Id, DisplayAreaFallback.Primary);
                var workArea = displayArea.WorkArea;

                var x = (workArea.Width - appWindow.Size.Width) / 2;
                var y = (workArea.Height - appWindow.Size.Height) / 2;

                appWindow.Move(new global::Windows.Graphics.PointInt32(x, y));
            }
        }
    }

    private void OnCloseClick(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private void OnEscapeAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        Hide();
        args.Handled = true;
    }

    private void OnRecordAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        if (ViewModel.ToggleRecordingCommand.CanExecute(null))
        {
            ViewModel.ToggleRecordingCommand.Execute(null);
        }

        args.Handled = true;
    }

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true)]
    private static extern bool ShowWindowNative(HWND hWnd, int nCmdShow);
}
