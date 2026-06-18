using System.ComponentModel;
using System.Text.Json;
using Microsoft.JSInterop;
using NameBadge.Models;

namespace NameBadge.Services;

public sealed class UserSettingsProvider(IJSRuntime jsRuntime)
{
    private const string KeyName = "NameBadge.Settings";
    private bool _initialized;
    private UserSettings? _settings;

    public event EventHandler? Changed;

    private bool AutoSave { get; set; } = true;

    public async ValueTask<UserSettings> Get()
    {
        if (_settings != null)  return _settings;

        // Register the Storage event handler. This handler calls OnStorageUpdated when the storage changed.
        // This way, you can reload the settings when another instance of the application (tab / window) save the settings
        if (!_initialized)
        {
            // Create a reference to the current object, so the JS function can call the public method "OnStorageUpdated"
            var reference = DotNetObjectReference.Create(this);
            await jsRuntime.InvokeVoidAsync("BlazorRegisterStorageEvent", reference);
            _initialized = true;
        }

        // Read the JSON string that contains the data from the local storage
        UserSettings result;
        var str = await jsRuntime.InvokeAsync<string>("BlazorGetLocalStorage", KeyName);
        if (!string.IsNullOrWhiteSpace(str))
        {
            try
            {
                result = JsonSerializer.Deserialize<UserSettings>(str) ?? new UserSettings();
            }
            catch (JsonException e)
            {
                Console.WriteLine(e);
                result = new UserSettings();
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e);
                result = new UserSettings();
            }
        }
        else
        {
            result = new UserSettings();
        }

        // Register the OnPropertyChanged event, so it automatically persists the settings as soon as a value is changed
        result.PropertyChanged += OnPropertyChanged;
        _settings = result;
        return result;
    }

    public async Task<bool> Save()
    {
        var json = JsonSerializer.Serialize(_settings);
        try
        {
            await jsRuntime.InvokeVoidAsync("BlazorSetLocalStorage", KeyName, json);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    // Automatically persist the settings when a property changed
    private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        try
        {
            if (AutoSave)
            {
                await Save();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    // This method is called from BlazorRegisterStorageEvent when the storage changed
    [JSInvokable]
    public void OnStorageUpdated(string key)
    {
        if (!KeyName.Equals(key, StringComparison.OrdinalIgnoreCase)) return;
        
        // Reset the settings. The next call to Get will reload the data
        _settings = null;
        Changed?.Invoke(this, EventArgs.Empty);
    }
}