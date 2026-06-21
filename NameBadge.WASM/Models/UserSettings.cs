using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NameBadge.Models;

public class UserSettings : INotifyPropertyChanged
{
    public string ProgramCode
    {
        get;
        set
        {
            field = value;
            RaisePropertyChanged();
        }
    } = string.Empty;

    public string EventName
    {
        get;
        set
        {
            field = value;
            RaisePropertyChanged();
        }
    } = string.Empty;

    public uint Year
    {
        get;
        set
        {
            field = value;
            RaisePropertyChanged();
        }
    }

    public bool IsComplete => !string.IsNullOrWhiteSpace(ProgramCode)
                              && !string.IsNullOrWhiteSpace(EventName)
                              && Year > 0;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}