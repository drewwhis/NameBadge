using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NameBadge.Models;

public class UserSettings : INotifyPropertyChanged
{
    public string ProgramType
    {
        get;
        set
        {
            field = value;
            RaisePropertyChanged();
        }
    } = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}