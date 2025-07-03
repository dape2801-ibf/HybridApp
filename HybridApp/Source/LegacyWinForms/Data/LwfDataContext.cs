using LegacyWinForms.BusinessObjects;

namespace LegacyWinForms.Data;
public static class LwfDataContext
{
    public static List<CustomerAddress> Customers { get; set; }

    public static List<Order> Orders { get; set; }
}
