using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NameBadge.Models;

public class UserSettings : INotifyPropertyChanged
{
    public string Username
    {
        get => field ?? string.Empty; 
        set
        {
            field = value;
            RaisePropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}