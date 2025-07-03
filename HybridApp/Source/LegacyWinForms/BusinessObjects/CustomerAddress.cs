using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LegacyWinForms.BusinessObjects;

public class CustomerAddress : INotifyPropertyChanged
{
    public int Id { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public string Straße { get; set; }
    public string Hausnummer { get; set; }
    public string PLZ { get; set; }
    public string Ort { get; set; }
    public string Land { get; set; }
    
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
