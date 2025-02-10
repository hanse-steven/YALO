using YALO.BLL.Models;

namespace YALO.BLL.Interfaces;

public interface IModuleService
{
    IEnumerable<Module> GetActiveModules();
}