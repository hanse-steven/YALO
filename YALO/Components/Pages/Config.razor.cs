using YALO.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using YALO.BLL.Interfaces;
using YALO.Domain.Models;
using YALO.Components.Dialogs;

namespace YALO.Components.Pages;

public partial class Config
{
    [Inject] public IJSRuntime JsRuntime { get; set; }
    [Inject] private IModuleService _moduleService { get; set; } 
    [Inject] private IDialogService _dialogService { get; set; }
    
    private IJSObjectReference JsReference { get; set; }
    private Module? _selected = null;

    private List<Module> _modules { get; set; } = [];

    protected override void OnInitialized()
    {
        _modules = _moduleService.LoadData().ToList();
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
            await JsReference.InvokeVoidAsync("addListenerRectangle");
            await JsReference.InvokeVoidAsync("resize");
        }
    }
    
    public async Task SavePosition()
    {
        foreach (PositionDimension pd in await JsReference.InvokeAsync<PositionDimension[]>("getPositionDimension"))
        {
            Module? module = _modules.FirstOrDefault(m => m.Id == Guid.Parse(pd.Id));
            if (module is not null)
            {
                module.X = pd.X;
                module.Y = pd.Y;
                module.Width = pd.Width;
                module.Height = pd.Height;
            }
        }
        _moduleService.SaveAll(_modules);
    }
    
    private void SelectModule(Module m)
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
    
    private async Task AddModule()
    {
        DialogOptions options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            FullWidth = true
        };
        IDialogReference dialog = await _dialogService.ShowAsync<AddModule>("Ajouter un module", options);
        DialogResult? result = await dialog.Result;
        if (!result.Canceled)
        {
            Module module = await dialog.GetReturnValueAsync<Module>();
            _modules.Add(module);
        }
    }

    private async Task EditModule()
    {
        DialogOptions options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            FullWidth = true
        };
        DialogParameters parameters = new DialogParameters
        {
            { "Module", _selected }
        };
        IDialogReference dialog = await _dialogService.ShowAsync<EditModule>("Modifier un module", parameters, options);
        DialogResult? result = await dialog.Result;
        if (!result.Canceled)
        {
            Module module = await dialog.GetReturnValueAsync<Module>();
            Module? moduleSelected = _modules.Find(m => m.Id == _selected.Id);
            if (moduleSelected is not null)
            {
                moduleSelected.Name = module.Name;
                moduleSelected.Parameters = module.Parameters;
            }
        }
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
            if (result == true)
            {
                this._modules.RemoveAll(m => m.Id == _selected.Id);
                UnselectItem();
            }
        }
    }

    private void ModifyWidth(int value)
    {
        if (_selected is not null)
        {
            Module? moduleSelected = _modules.Find(m => m.Id == _selected.Id);
            
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
            Module? moduleSelected = _modules.Find(m => m.Id == _selected.Id);
            
            if (moduleSelected is not null)
            {
                moduleSelected.Height += value;
                if (moduleSelected.Height <= 0) moduleSelected.Height = 1;
            }
        }
    }
}