namespace LegacyWinForms.Data;
internal static class DataModule
{
    public static void InitializeDataMode()
    {
        LwfDataContext.Customers = CustomerAddressGenerator.Generate(50);
        LwfDataContext.Orders = OrderGenerator.Generate(100, LwfDataContext.Customers);
    }
}
