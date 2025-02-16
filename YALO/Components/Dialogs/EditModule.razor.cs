using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using YALO.Domain.Models;

namespace YALO.Components.Dialogs;

public partial class EditModule
{
    [Parameter] public Module Module { get; set; }
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; }

    
    private void Cancel() => MudDialog.Cancel();
    
    private void Submit()
    {
        MudDialog.Close<Module>(Module);
    }

    private void OnValidSubmit(EditContext context)
    {
        
    }
}