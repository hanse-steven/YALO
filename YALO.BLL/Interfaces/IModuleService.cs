using YALO.Domain.Models;

namespace YALO.BLL.Interfaces;

public interface IModuleService
{
    public void SaveAll(IEnumerable<Module> modules);
    public IEnumerable<Module> LoadData();
}