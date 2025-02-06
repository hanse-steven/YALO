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
        await JsReference.InvokeVoidAsync("resize");
    }
    

    public string GetStyle(ModuleDTO m)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"top: {m.Y}px;");
        sb.Append($"left: {m.X}px;");
        sb.Append($"width: {m.Width}px;");
        sb.Append($"height: {m.Height}px;");
        sb.Append($"line-height: {m.Height}px;");
        sb.Append($"background-color: {m.Color};");
        return sb.ToString();
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