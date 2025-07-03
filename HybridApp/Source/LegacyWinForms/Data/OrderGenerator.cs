using LegacyWinForms.BusinessObjects;

namespace LegacyWinForms.Data;
internal static class OrderGenerator
{
    private static readonly string[] StatusList = { "Offen", "In Bearbeitung", "Versendet", "Abgeschlossen", "Storniert" };
    private static readonly Random rnd = new();

    public static List<Order> Generate(int count, List<CustomerAddress> customers)
    {
        var list = new List<Order>();
        for (int i = 1; i <= count; i++)
        {
            var customer = customers[rnd.Next(customers.Count)];
            var order = new Order
            {
                Id = i,
                CustomerId = customer.Id,
                Bestellnummer = $"ORD{rnd.Next(10000, 99999)}",
                Bestelldatum = DateTime.Today.AddDays(-rnd.Next(0, 365)),
                Betrag = Math.Round((decimal)rnd.NextDouble() * 1000 + 50, 2),
                Status = StatusList[rnd.Next(StatusList.Length)]
            };
            list.Add(order);
        }
        return list;
    }
}
