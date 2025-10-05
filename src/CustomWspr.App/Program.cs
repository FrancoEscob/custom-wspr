using System;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.ApplicationModel.DynamicDependency;
using System.Threading;
using WinRT;

namespace CustomWspr.App;

public static class Program
{
    private const uint WindowsAppSdkMajorMinor = 0x00010006;

    [STAThread]
    public static void Main(string[] args)
    {
        ComWrappersSupport.InitializeComWrappers();

        if (!Bootstrap.TryInitialize(WindowsAppSdkMajorMinor, out var bootstrapHresult))
        {
            var message = $"Bootstrap initialization failed with HRESULT 0x{bootstrapHresult:X8}";
            System.Diagnostics.Debug.WriteLine(message);
            throw new InvalidOperationException(message);
        }

        try
        {
            Application.Start(p =>
            {
                var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
                SynchronizationContext.SetSynchronizationContext(context);
                new App();
            });
        }
        finally
        {
            Bootstrap.Shutdown();
        }
    }
}
