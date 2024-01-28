using Android.App;
using Android.Runtime;

namespace ToDoList.Client;

// Note should be removed when moving to https
#if DEBUG
[Application(UsesCleartextTraffic = true)]
#else
[Application]                               
#endif
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
