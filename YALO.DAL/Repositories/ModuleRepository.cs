using System.Text;
using System.Text.Json;
using YALO.DAL.Entities;
using YALO.DAL.Interfaces;

namespace YALO.DAL.Repositories;

public class ModuleRepository : IModuleRepository
{
    public IEnumerable<Module> GetAll()
    {
        string data = File.ReadAllText("Modules.json", Encoding.UTF8);
        Module[] modules = JsonSerializer.Deserialize<Module[]>(data)!;
        return modules;
    }
    
    public void SaveAll(IEnumerable<Module> modules)
    {
        string data = JsonSerializer.Serialize(modules);
        File.WriteAllText("Modules.json", data, Encoding.UTF8);
    }
}