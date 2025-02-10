using YALO.BLL.Interfaces;
using YALO.BLL.Models;
using YALO.BLL.Modules;

namespace YALO.BLL.Services;

public class ModuleService : IModuleService
{
    public IEnumerable<Module> AvailableModules = 
    [
        new WebContainerModule()
    ];
    
    public IEnumerable<Module> GetActiveModules()
    {
        return this.AvailableModules;
    }
}