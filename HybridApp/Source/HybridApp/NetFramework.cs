using DevExpress.Utils;
using DevExpress.Utils.Diagnostics;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Security;

namespace HybridApp;
internal class NetFramework
{
    public static void ConfigureRuntime()
    {
        AppContext.SetSwitch("Switch.System.Globalization.NoAsyncCurrentCulture", true);
        AppContext.SetSwitch("Switch.UseLegacyToolTipDisplay", true);
        AppContext.SetSwitch("Switch.System.Windows.Media.EnableHardwareAccelerationInRdp", true);

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Control.CheckForIllegalCrossThreadCalls = false;

        WindowsFormsSettings.AllowAutoScale = DefaultBoolean.True;
        WindowsFormsSettings.AllowDpiScale = true;
        WindowsFormsSettings.SetPerMonitorDpiAware();

        WindowsFormsSettings.ForceAPIPaint();
        WindowsFormsSettings.ForcePaintApiDiagnostics(PaintApiDiagnosticsLevel.Trace);

        WindowsFormsSettings.OptimizeRemoteConnectionPerformance =
            SystemInformation.TerminalServerSession ? DefaultBoolean.True : DefaultBoolean.False;

        ScriptPermissionManager.GlobalInstance = new ScriptPermissionManager(ExecutionMode.Unrestricted);
    }
}
