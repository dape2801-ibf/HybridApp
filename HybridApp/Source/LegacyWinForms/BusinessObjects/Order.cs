using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LegacyWinForms.BusinessObjects;

public class Order : INotifyPropertyChanged
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Bestellnummer { get; set; }
    public DateTime Bestelldatum { get; set; }
    public decimal Betrag { get; set; }
    public string Status { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
