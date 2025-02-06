using YALO.DAL.Entities;

namespace YALO.DAL.Interfaces;

public interface IModuleRepository
{
    IEnumerable<Module> GetAll();
    public void SaveAll(IEnumerable<Module> modules);
    public void DeleteById(int id);
}