using Microsoft.AspNetCore.Components;
using NameBadge.Models;

namespace NameBadge.Shared;

public partial class UserSettingsComponent : ComponentBase, IDisposable
{
    private UserSettings? _state;

    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UserSettingsProvider.Changed += UserSettingsChanged;
        await Refresh();
    }

    public void Dispose()
    {
        UserSettingsProvider.Changed -= UserSettingsChanged;
    }

    private async void UserSettingsChanged(object? sender, EventArgs e)
    {
        try
        {
            await InvokeAsync(async () =>
            {
                await Refresh();
                StateHasChanged();
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task Refresh()
    {
        _state = await UserSettingsProvider.Get();
    }
}