using LegacyWinForms.BusinessObjects;

namespace LegacyWinForms.Data;
internal static class CustomerAddressGenerator
{
    private static readonly string[] Vornamen = { "Max", "Anna", "Peter", "Julia", "Lukas", "Laura" };
    private static readonly string[] Nachnamen = { "Müller", "Schmidt", "Schneider", "Fischer", "Weber", "Meyer" };
    private static readonly string[] Straßen = { "Hauptstraße", "Bahnhofstraße", "Gartenweg", "Schulstraße", "Ringstraße" };
    private static readonly string[] Orte = { "Berlin", "München", "Hamburg", "Köln", "Frankfurt" };
    private static readonly string[] Länder = { "Deutschland", "Österreich", "Schweiz" };
    private static readonly Random rnd = new();

    public static List<CustomerAddress> Generate(int count)
    {
        var list = new List<CustomerAddress>();
        for (int i = 1; i <= count; i++)
        {
            var address = new CustomerAddress
            {
                Id = i,
                Vorname = Vornamen[rnd.Next(Vornamen.Length)],
                Nachname = Nachnamen[rnd.Next(Nachnamen.Length)],
                Straße = Straßen[rnd.Next(Straßen.Length)],
                Hausnummer = rnd.Next(1, 200).ToString(),
                PLZ = rnd.Next(10000, 99999).ToString(),
                Ort = Orte[rnd.Next(Orte.Length)],
                Land = Länder[rnd.Next(Länder.Length)]
            };
            list.Add(address);
        }
        return list;
    }
}
