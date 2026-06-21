using Microsoft.AspNetCore.Components;
using NameBadge.Models;
using Radzen;

namespace NameBadge.Pages;

public partial class Home : ComponentBase
{
    [CascadingParameter]
    private UserSettings UserSettings { get; set; } = new();

    private ButtonStyle GetSettingsButtonStyle()
    {
        return UserSettings.IsComplete ? ButtonStyle.Base : ButtonStyle.Danger;
    }

    private string DisplayMissingSettings()
    {
        var programCode = UserSettings.ProgramCode;
        var eventName = UserSettings.EventName;
        var year = UserSettings.Year;

        if (string.IsNullOrWhiteSpace(programCode))
        {
            return "You must select a program.";
        }

        if (string.IsNullOrWhiteSpace(eventName))
        {
            return "You must supply a specific event name.";
        }

        if (year == 0)
        {
            return "You must specify a year for the event.";
        }

        var program =
            ProgramType.ProgramTypes.FirstOrDefault(p =>
                p.Code.Equals(programCode, StringComparison.OrdinalIgnoreCase));
        return program is null ? "Irrecoverable error." : string.Empty;
    }
}