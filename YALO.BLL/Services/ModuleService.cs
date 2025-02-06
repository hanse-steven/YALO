using YALO.BLL.Interfaces;
using YALO.BLL.Mappers;
using YALO.BLL.Models;
using YALO.DAL.Interfaces;

namespace YALO.BLL.Services;

public class ModuleService : IModuleService
{
    private readonly IModuleRepository _moduleRepository;
    public ModuleService(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public IEnumerable<Module> GetAll()
    {
        return _moduleRepository.GetAll().Select(m => m.ToModel());
    }

    public void SaveAll(IEnumerable<Module> modules)
    {
        _moduleRepository.SaveAll(modules.Select(m => m.ToEntity()));
    }

    public void Delete(int id)
    {
        _moduleRepository.DeleteById(id);
    }
}