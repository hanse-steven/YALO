using YALO.Domain.Models;

namespace YALO.DAL.Interfaces;

public interface IModuleRepository
{
    public IEnumerable<Module> GetAll();
    public void SaveAll(IEnumerable<Module> modules);
}