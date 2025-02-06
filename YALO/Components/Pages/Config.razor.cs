using System.Text;
using YALO.BLL.Interfaces;
using YALO.DTOs;
using YALO.Mappers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace YALO.Components.Pages;

public partial class Config
{
    [Inject] public IJSRuntime JsRuntime { get; set; }
    private IJSObjectReference JsReference { get; set; }
    [Inject] private IModuleService _moduleService { get; set; } 
    
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
        _moduleService.SaveAll(modules.Select(m => m.ToModel()));
    }
}