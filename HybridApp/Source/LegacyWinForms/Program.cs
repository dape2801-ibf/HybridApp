using LegacyWinForms.Data;

namespace LegacyWinForms;

public static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }

    /// <summary>
    /// This is the initialization method for the hybrid application.
    /// Use this to initialize the application settings, skins, and data context.
    /// </summary>
    public static void InitApplication()
    {
        Application.EnableVisualStyles();
        DevExpress.Skins.SkinManager.EnableFormSkins();

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.SetCompatibleTextRenderingDefault(false);

        SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
        DataModule.InitializeDataMode();
    }
}