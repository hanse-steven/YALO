using Microsoft.AspNetCore.Components;
using MudBlazor;
using YALO.Domain.Models;

namespace YALO.Components.Dialogs;

public partial class AddModule
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; }
    
    private Type? _selectedValue;
    private string? _name;
    private Dictionary<string, string> _parameterValues = [];
    private readonly Dictionary<Type, Dictionary<string, Type>> _parametersRegistry = Module.GetAvailableParameters();

    private void Cancel() => MudDialog.Cancel();
    
    private void Submit()
    {
        if (_selectedValue is null) return;
        
        Module? module = Activator.CreateInstance(_selectedValue!) as Module;
        if (module is null) return;
        
        module.Name = _name ?? "";
        foreach (var (key, value) in _parameterValues)
        {
            module.Parameters[key] = value;
        }
        
        MudDialog.Close<Module>(module);
    }
    
    private void SelectedValueChanged()
    {
        _parameterValues.Clear();
    }
    
    private void ValueChanged(string name, string value)
    {
        _parameterValues[name] = value;
    }
}