namespace LegacyWinForms.Connector;
internal class LegacyWinFormsStartUp
{
    public Task StartEmbedded()
    {
        Program.InitApplication();
        return Task.CompletedTask;
    }
}
