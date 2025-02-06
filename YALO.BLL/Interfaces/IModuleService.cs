using YALO.BLL.Models;

namespace YALO.BLL.Interfaces;

public interface IModuleService
{
    public IEnumerable<Module> GetAll();
    public void SaveAll(IEnumerable<Module> modules);
    public void Delete(int id);
}