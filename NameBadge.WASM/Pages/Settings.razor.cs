using Microsoft.AspNetCore.Components;
using NameBadge.Models;

namespace NameBadge.Pages;

public partial class Settings : ComponentBase
{
    [CascadingParameter]
    private UserSettings UserSettings { get; set; } = new();
}