using YALO.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using YALO.BLL.Interfaces;
using YALO.BLL.Services;
using YALO.Mappers;

namespace YALO.Components.Pages;

public partial class Config
{
    [Inject] public IJSRuntime JsRuntime { get; set; }
    [Inject] private IModuleService _moduleService { get; set; } 
    [Inject] private IDialogService _dialogService { get; set; }
    
    private IJSObjectReference JsReference { get; set; }
    private ModuleDTO? _selected = null;
    private List<ModuleDTO> _modules { get; set; } = [];

    protected override void OnInitialized()
    {
        _modules = _moduleService.GetActiveModules().Select(m => m.ToDTO()).ToList();
        
        foreach (ModuleDTO module in _modules)
        {
            foreach ((string? key, ParameterDTO? value) in module.Parameters)
            {
                Console.Out.WriteLineAsync($"{key}: {value}");
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            JsReference = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "../Components/Pages/Config.razor.js");
            await JsReference.InvokeVoidAsync("addHandlers");
        }
        else
        {
            await JsReference.InvokeVoidAsync("resize");
        }
    }
    
    public async Task SavePosition()
    {
        List<ModuleDTO> modules = [];
        foreach (var moduleDto in await JsReference.InvokeAsync<ModuleDTO[]>("getPositionDimension"))
        {
            modules.Add(moduleDto);
        }
        //_moduleService.SaveAll(modules.Select(m => m.ToModel()));
    }
    
    private void SelectModule(ModuleDTO m)
    {
        this._selected = m; 
        StateHasChanged();
    }
    
    private void UnselectItem()
    {
        this._selected = null;
        JsReference.InvokeVoidAsync("unselectItem");
        StateHasChanged();
    }

    private async Task DeleteModule()
    {
        if (_selected is not null)
        {
            bool? result = await _dialogService.ShowMessageBox(
                "Suppression", 
                "Voulez-vous vraiment supprimer ce module ?", 
                yesText: "Oui", 
                cancelText: "Non"
            );
            if (result == true) ;
            //this._moduleService.Delete(_selected.Id);
        }
    }

    private void ModifyWidth(int value)
    {
        if (_selected is not null)
        {
            ModuleDTO? moduleSelected = _modules.Find(m => m.Id == _selected.Id);

            if (moduleSelected is not null)
            {
                moduleSelected.Width += value;
                if (moduleSelected.Width <= 0) moduleSelected.Width = 1;
            }
        }
    }
    
    private void ModifyHeight(int value)
    {
        if (_selected is not null)
        {
            ModuleDTO? moduleSelected = _modules.Find(m => m.Id == _selected.Id);

            if (moduleSelected is not null)
            {
                moduleSelected.Height += value;
                if (moduleSelected.Height <= 0) moduleSelected.Height = 1;
            }
        }
    }
}