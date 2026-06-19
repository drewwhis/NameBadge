using Microsoft.AspNetCore.Components;
using NameBadge.Models;

namespace NameBadge.Pages;

public partial class Home : ComponentBase
{
    [CascadingParameter]
    private UserSettings UserSettings { get; set; } = new();

    private string DisplayEventData()
    {
        var programCode = UserSettings?.ProgramCode;
        var eventName = UserSettings?.EventName;

        if (string.IsNullOrWhiteSpace(programCode))
        {
            return "You must select a program.";
        }

        if (string.IsNullOrWhiteSpace(eventName))
        {
            return "You must supply a specific event name.";
        }

        var program =
            ProgramType.ProgramTypes.FirstOrDefault(p =>
                p.Code.Equals(programCode, StringComparison.OrdinalIgnoreCase));
        if (program is null)
        {
            return "Irrecoverable error.";
        }

        return $"{program.DisplayName} {eventName}";
    }
}